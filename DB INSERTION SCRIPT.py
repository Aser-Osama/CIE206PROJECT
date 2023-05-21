import pyodbc
import random
from faker import Faker

fake = Faker()

# Database connectionpyodbc.connect("Driver={SQL Server};Server=ASERLAPTOP;Database=db_proj_new;Trusted_Connection=yes;"
conn=pyodbc.connect("Driver={SQL Server};Server=LAPTOP-EOAIN8PD;Database=THIS_IS_THE_DATABASE;Trusted_Connection=yes;")

cursor = conn.cursor()

# Functions to generate random data
def random_user_type():
    return random.choice(['Student', 'Parent', 'Trainer', 'Sales', 'op_mngr', 'CEO', 'coordinator', 'content_developer', 'supervisor', 'senior_supervisor'])

def random_level():
    return random.choice(['beginner', 'intermediate', 'advanced'])

def random_field():
    return random.choice(['math', 'science', 'history', 'english'])

def random_age_grp():
    return random.choice(['young', 'middle', 'old'])

def random_one_two_time():
    return random.choice(['one', 'two'])
# Functions to insert data into tables
def insert_course(course_id):
    cursor.execute("""
        INSERT INTO [course] (course_id, course_name, course_description, tot_sessions, advertisement_text, video_link)
        VALUES (?, ?, ?, ?, ?, ?)
    """, (course_id, fake.word(), fake.text(max_nb_chars=100), random.randint(1, 20), fake.text(max_nb_chars=1000), fake.url()))

def insert_user(user_id):
    cursor.execute("""
        INSERT INTO "user" (user_id, date_of_birth, profile_pic, join_date, "address", "name", password, email, user_type)
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)
    """, (user_id, fake.date_of_birth().strftime('%Y-%m-%d'), fake.file_name(), fake.date_between().strftime('%Y-%m-%d'), fake.address(), fake.name(), fake.password(), fake.email(), random_user_type()))

def insert_trainer(user_id):
    cursor.execute("""
        INSERT INTO Trainer (user_id, [level], field)
        VALUES (?, ?, ?)
    """, (user_id, random_level(), random_field()))

def insert_offering(offering_id, course_id):
    cursor.execute("""
        INSERT INTO offering (offering_id, course_id, Start_Date, Price)
        VALUES (?, ?, ?, ?)
    """, (offering_id, course_id, fake.date_between().strftime('%Y-%m-%d'), random.randint(100, 1000)))

def insert_group(group_no, offering_id, trainer_id):
    cursor.execute("""
        INSERT INTO "group" (group_no, offering_id, Trainer_id, Timeslot, meeting_link, age_grp)
        VALUES (?, ?, ?, ?, ?, ?)
    """, (group_no, offering_id, trainer_id, fake.date_time_this_month().strftime('%Y-%m-%d %H:%M:%S'), fake.url(), random_age_grp()))

def insert_student(user_id, parent_id):
    cursor.execute("""
        INSERT INTO Student (user_id, parent_id, skill_level)
        VALUES (?, ?, ?)
    """, (user_id, parent_id, random_level()))

def insert_student_groups(group_no, student_id):
    cursor.execute("""
        INSERT INTO Student_groups (group_no, Student_id)
        VALUES (?, ?)
    """, (group_no, student_id))
def insert_request(request_id, sent_by, sent_to):
    cursor.execute("""
        INSERT INTO request (request_id, content, subject, datetime, sent_by, sent_to)
        VALUES (?, ?, ?, ?, ?, ?)
    """, (request_id, fake.text(max_nb_chars=100), fake.word(), fake.date_time_this_year().strftime('%Y-%m-%d'), sent_by, sent_to))

def insert_content(content_id, course_id):
    cursor.execute("""
        INSERT INTO content (content_id, course_id, summary, summary_vid, slides, teacher_guide, handout)
        VALUES (?, ?, ?, ?, ?, ?, ?)
    """, (content_id, course_id, fake.text(max_nb_chars=1000), fake.url(), fake.file_name(), fake.file_name(), fake.file_name()))

def insert_lecture(lecture_id, content_id, group_id):
    cursor.execute("""
        INSERT INTO lecture (lecture_id, content_id, group_id, day, room)
        VALUES (?, ?, ?, ?, ?)
    """, (lecture_id, content_id, group_id, fake.date_between().strftime('%Y-%m-%d'), fake.word()))

def insert_content_topics(content_id):
    cursor.execute("""
        INSERT INTO content_topics (content_id, topic, topic_description)
        VALUES (?, ?, ?)
    """, (content_id, fake.word(), fake.text(max_nb_chars=500)))


# Set to store existing (student_id, lecture_id) combinations
existing_student_lecture_pairs = set()


def insert_student_eval(student_id, lecture_id):
    # Check if the (student_id, lecture_id) combination exists in the set
    if (student_id, lecture_id) not in existing_student_lecture_pairs:
        cursor.execute("""
            INSERT INTO student_eval (student_id, lecture_id, attendance, criteria_c1, criteria_c2, criteria_c3, criteria_c4, date)
            VALUES (?, ?, ?, ?, ?, ?, ?, ?)
        """, (student_id, lecture_id, random.choice([True, False]), random.randint(1, 5), random.randint(1, 5),
              random.randint(1, 5), random.randint(1, 5), fake.date_time_this_year().strftime('%Y-%m-%d')))

        # Add the new (student_id, lecture_id) pair to the set
        existing_student_lecture_pairs.add((student_id, lecture_id))
    else:
        print(f"Skipping duplicate (student_id, lecture_id) pair: ({student_id}, {lecture_id})")

# Set to store existing (parent_id, group_id) combinations
existing_course_payment_pairs = set()

def insert_course_payment(parent_id, group_id, transaction_no):
    # Check if the (parent_id, group_id) combination exists in the set
    if (parent_id, group_id) not in existing_course_payment_pairs:
        cursor.execute("""
            INSERT INTO course_payment (parent_id, group_id, transaction_no, one_two_time, v_cash_msg, amount_payed)
            VALUES (?, ?, ?, ?, ?, ?)
        """, (parent_id, group_id, transaction_no, random_one_two_time(), fake.text(max_nb_chars=100), random.randint(100, 1000)))

        # Add the new (parent_id, group_id) pair to the set
        existing_course_payment_pairs.add((parent_id, group_id))
    else:
        print(f"Skipping duplicate (parent_id, group_id) pair: ({parent_id}, {group_id})")

def insert_phone_num(user_id):
    cursor.execute("""
        INSERT INTO phone_num (user_id, phone_num)
        VALUES (?, ?)
    """, (user_id, fake.phone_number()))

# Set to store existing lecture_ids
existing_trainer_eval_lecture_ids = set()

def insert_trainer_eval(lecture_id):
    # Check if the lecture_id exists in the set
    if lecture_id not in existing_trainer_eval_lecture_ids:
        cursor.execute("""
            INSERT INTO trainer_eval (lecture_id, criteria_c1, criteria_c2, criteria_c3, criteria_c4, date, attended)
            VALUES (?, ?, ?, ?, ?, ?, ?)
        """, (lecture_id, random.randint(1, 5), random.randint(1, 5), random.randint(1, 5), random.randint(1, 5), fake.date_time_this_year().strftime('%Y-%m-%d'), random.randint(1, num_students)))

        # Add the new lecture_id to the set
        existing_trainer_eval_lecture_ids.add(lecture_id)
    else:
        print(f"Skipping duplicate lecture_id: {lecture_id}")

def insert_salary_pt(user_id):
    cursor.execute("""
        INSERT INTO salary_pt (user_id, hours_worked, pay_per_session, pay_in_month)
        VALUES (?, ?, ?, ?)
    """, (user_id, random.randint(1, 160), random.randint(10, 50), random.randint(100, 5000)))

def insert_salary_ft(user_id):
    cursor.execute("""
        INSERT INTO salary_ft (user_id, OT_TIME_OFF, monthly)
        VALUES (?, ?, ?)
    """, (user_id, random.randint(1, 10), random.randint(1000, 10000)))



# Generate and insert data
num_courses = 5
num_users = 100
num_trainers = 20
num_offerings = 10
num_groups = 20
num_students = 50
num_requests = 50
num_contents = 20
num_lectures = 100
num_student_evals = 200
num_course_payments = 75
num_phone_nums = 80
num_trainer_evals = 100
num_salary_pts = 60
num_salary_fts = 40

for course_id in range(1, num_courses+1):
    insert_course(course_id)

for user_id in range(1, num_users+1):
    insert_user(user_id)

    if user_id <= num_trainers:
        insert_trainer(user_id)

for offering_id in range(1, num_offerings+1):
    insert_offering(offering_id, random.randint(1, num_courses))

for group_no in range(1, num_groups+1):
    insert_group(group_no, random.randint(1, num_offerings), random.randint(1, num_trainers))

for student_id in range(1, num_students+1):
    insert_student(student_id, random.randint(1, num_users))
    insert_student_groups(random.randint(1, num_groups), student_id)


for request_id in range(1, num_requests+1):
    insert_request(request_id, random.randint(1, num_users), random.randint(1, num_users))


for content_id in range(1, num_contents+1):
    insert_content(content_id, random.randint(1, num_courses))
    insert_content_topics(content_id)

for lecture_id in range(1, num_lectures+1):
    insert_lecture(lecture_id, random.randint(1, num_contents), random.randint(1, num_groups))

for student_eval_id in range(1, num_student_evals+1):
    insert_student_eval(random.randint(1, num_students), random.randint(1, num_lectures))

for course_payment_id in range(1, num_course_payments+1):
    insert_course_payment(random.randint(1, num_users), random.randint(1, num_groups), course_payment_id)

for phone_num_id in range(1, num_phone_nums+1):
    insert_phone_num(random.randint(1, num_users))

for trainer_eval_id in range(1, num_trainer_evals+1):
    insert_trainer_eval(random.randint(1, num_lectures))

for salary_pt_id in range(1, num_salary_pts+1):
    insert_salary_pt(random.randint(1, num_users))

for salary_ft_id in range(1, num_salary_fts+1):
    insert_salary_ft(random.randint(1, num_users))

# Commit the changes and close the connection
conn.commit()
conn.close()
