SELECT "group".group_no, course.course_name, "user"."name" AS tutor_name
FROM Student_groups
JOIN "group" ON Student_groups.group_no = "group".group_no
JOIN offering ON "group".offering_id = offering.offering_id
JOIN course ON offering.course_id = course.course_id
JOIN Trainer ON "group".Trainer_id = Trainer.user_id
JOIN "user" ON Trainer.user_id = "user".user_id
WHERE Student_groups.Student_id = <student_id>;




SELECT "user"."name" AS student_name, "user".profile_pic 
FROM Student_groups
JOIN "user" ON Student_groups.Student_id = "user".user_id
WHERE Student_groups.group_no = <group_no>;


SELECT COUNT(Student_groups.Student_id) AS num_students
FROM Student_groups
WHERE Student_groups.group_no = <group_no>;

SELECT
  course.course_name,
  course.course_description,
  course.tot_sessions,
  offering.Start_Date,
  offering.Price,
  "group".group_no,
  "group".Trainer_id,
  "group".Timeslot,
  "group".n_students,
  "group".meeting_link,
  "group".age_grp,
  "user"."name" AS tutor_name,
  "user".email AS tutor_email
FROM "group"
JOIN offering ON "group".offering_id = offering.offering_id
JOIN course ON offering.course_id = course.course_id
LEFT JOIN Trainer ON "group".Trainer_id = Trainer.user_id
LEFT JOIN "user" ON Trainer.user_id = "user".user_id
WHERE "group".group_no = <group_no>;




SELECT
  content_topics.topic,
  content_topics.topic_description
FROM content_topics
WHERE content_topics.content_id = <content_id>;


SELECT
  content.content_id,
  content.summary,
  content.summary_vid,
  content.slides,
  content.teacher_guide,
  content.handout
FROM content
JOIN course ON content.course_id = course.course_id
JOIN offering ON course.course_id = offering.course_id
JOIN "group" ON offering.offering_id = "group".offering_id
WHERE "group".group_no = <group_no>;


SELECT *
FROM "user"
WHERE user_id = <user_id>;


SELECT g.group_no,
    AVG(se.attendance) AS avg_attendance,
    AVG(se.criteria_c1) AS avg_criteria_c1,
    AVG(se.criteria_c2) AS avg_criteria_c2,
    AVG(se.criteria_c3) AS avg_criteria_c3,
    AVG(se.criteria_c4) AS avg_criteria_c4
FROM "group" g
JOIN lecture l ON g.group_no = l.group_id
LEFT JOIN student_eval se ON l.lecture_id = se.lecture_id
WHERE se.student_id = <student_id>
GROUP BY g.group_no;

SELECT g.group_no,
    AVG(te.criteria_c1) AS avg_criteria_c1,
    AVG(te.criteria_c2) AS avg_criteria_c2,
    AVG(te.criteria_c3) AS avg_criteria_c3,
    AVG(te.criteria_c4) AS avg_criteria_c4
FROM "group" g
JOIN lecture l ON g.group_no = l.group_id
LEFT JOIN trainer_eval te ON l.lecture_id = te.lecture_id
WHERE l.trainer_id = <trainer_id>
GROUP BY g.group_no;


SELECT *
FROM request
WHERE sent_to = <user_id>;

DELETE FROM request
WHERE sent_to = <user_id>;
