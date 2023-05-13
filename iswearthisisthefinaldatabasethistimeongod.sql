CREATE TABLE course (
    course_id INT PRIMARY KEY,
    tot_sessions INT,
    advertisement_text VARCHAR(2000),
    video_link VARCHAR(200),
);

CREATE TABLE "user" (
    user_id INT PRIMARY KEY,
    date_of_birth DATE,
    join_date DATE,
    "address" VARCHAR(255),
    "name" VARCHAR(255),
    password VARCHAR(255),
    email VARCHAR(255),
    user_type VARCHAR(20) CHECK (user_type IN ('Student', 'Parent', 'Trainer', 'Sales', 'op_mngr', 'CEO', 'coordinator', 'content_developer', 'supervisor', 'senior_supervisor'))
	);

CREATE TABLE Trainer (
    user_id INT PRIMARY KEY,
    [level] VARCHAR(20) CHECK ([level] IN ('beginner', 'intermediate', 'advanced')),
    field VARCHAR(20) CHECK (field IN ('math', 'science', 'history', 'english')),
    FOREIGN KEY (user_id) REFERENCES "user"(user_id) ON DELETE CASCADE
);

CREATE TABLE offering (
    offering_id INT PRIMARY KEY,
    course_id INT,
    Start_Date DATE,
    Price INT,
    FOREIGN KEY (course_id) REFERENCES course(course_id) ON DELETE CASCADE
)

CREATE TABLE "group" (
    group_no INT PRIMARY KEY,
    offering_id INT,
    Trainer_id INT,
    Timeslot DATE,
    n_students INT,
    meeting_link VARCHAR(255),
    age_grp VARCHAR(10) CHECK (age_grp IN ('young', 'middle', 'old')),
    FOREIGN KEY (offering_id) REFERENCES offering(offering_id) ON DELETE CASCADE,
    FOREIGN KEY (Trainer_id) REFERENCES Trainer(user_id) ON DELETE SET NULL
);

CREATE TABLE Student (
    user_id INT PRIMARY KEY,
    parent_id INT,
    skill_level VARCHAR(20) CHECK (skill_level IN ('beginner', 'intermediate', 'advanced')),
    FOREIGN KEY (user_id) REFERENCES "user"(user_id) ON DELETE CASCADE,
    FOREIGN KEY (parent_id) REFERENCES "user"(user_id) ON DELETE NO ACTION,
);

CREATE TABLE Student_groups(
    group_no INT,
    Student_id INT,
    PRIMARY KEY (group_no,student_id),
    FOREIGN KEY (Student_id) REFERENCES Student(user_id) ON DELETE CASCADE,
    FOREIGN KEY (group_no) REFERENCES "group"(group_no) ON DELETE CASCADE
)

CREATE TABLE request (
    request_id INT PRIMARY KEY,
    content VARCHAR(255),
    subject VARCHAR(255),
    datetime DATETIME,
    sent_by INT,
    sent_to INT,
    FOREIGN KEY (sent_by) REFERENCES "user"(user_id) ON DELETE NO ACTION,
    FOREIGN KEY (sent_to) REFERENCES "user"(user_id) ON DELETE NO ACTION
);

CREATE TABLE content (
    content_id INT PRIMARY KEY,
    course_id INT,
    summary VARCHAR(2000),
    summary_vid VARCHAR(255),
    slides VARCHAR(255),
    teacher_guide VARCHAR(255),
    handout VARCHAR(255),
    FOREIGN KEY (course_id) REFERENCES course(course_id) ON DELETE SET NULL
);

CREATE TABLE lecture (
    lecture_id INT,
    content_id INT,
    group_id INT,
    day DATE,
    room VARCHAR(255),
    PRIMARY KEY (lecture_id),
    FOREIGN KEY (group_id) REFERENCES "group"(group_no) ON DELETE CASCADE,
    FOREIGN KEY (content_id) REFERENCES content(content_id) ON DELETE CASCADE
);

CREATE TABLE lecture_topics (
    lecture_id INT,
    Topic VARCHAR(255),
    Topic_Description VARCHAR(1000),
    FOREIGN KEY (lecture_id) REFERENCES lecture(lecture_id) ON DELETE CASCADE
)



CREATE TABLE student_eval (
    student_id INT,
    lecture_id INT,
    attendance BIT,
    criteria_c1 INT,
    criteria_c2 INT,
    criteria_c3 INT,
    criteria_c4 INT,
    date DATETIME,
    PRIMARY KEY (student_id, lecture_id),
    FOREIGN KEY (student_id) REFERENCES Student(user_id) ON DELETE CASCADE,
    FOREIGN KEY (lecture_id) REFERENCES lecture(lecture_id) ON DELETE CASCADE
);

CREATE TABLE course_payment (
    parent_id INT,
    group_id INT,
    transaction_no INT,
    one_two_time VARCHAR(3) CHECK (one_two_time IN ('one', 'two')),
    v_cash_msg VARCHAR(255),
    amount_payed INT,
    PRIMARY KEY (parent_id, group_id),
    FOREIGN KEY (parent_id) REFERENCES "user"(user_id) ON DELETE CASCADE,
    FOREIGN KEY (group_id) REFERENCES "group"(group_no) ON DELETE CASCADE
);

CREATE TABLE phone_num (
    user_id INT,
    phone_num VARCHAR(255) UNIQUE,
    FOREIGN KEY (user_id) REFERENCES "user"(user_id) ON DELETE CASCADE
);

CREATE TABLE  trainer_eval(
    lecture_id INT,
    criteria_c1 INT,
    criteria_c2 INT,
    criteria_c3 INT,
    criteria_c4 INT,
    date DATETIME,
    attended INT,
    PRIMARY KEY (lecture_id),
    FOREIGN KEY (lecture_id) REFERENCES lecture(lecture_id) ON DELETE CASCADE
);

CREATE TABLE salary_pt (
    user_id INT,
    hours_worked INT,
    pay_per_session INT,
    pay_in_month INT,
    FOREIGN KEY (user_id) REFERENCES "user"(user_id) ON DELETE CASCADE 
);

CREATE TABLE salary_ft (
    user_id INT,
    OT_TIME_OFF INT,
    monthly INT,
    FOREIGN KEY (user_id) REFERENCES "user"(user_id) ON DELETE CASCADE
);