CREATE DATABASE InLettLX
ON PRIMARY
(    
    NAME='InLettLX',
	FILENAME='D:\SQL\0310练习\InLettLX.mdf',
	SIZE=10MB,
	FILEGROWTH=10%
)
LOG ON
(
   NAME='InLettLX_log',
   FILENAME='D:\SQL\0310练习\InLettLX_log.ldf',
   SIZE=10MB,
   FILEGROWTH=10%
)
USE InLettLX
GO
CREATE TABLE Student --学生表
(
  SId VARCHAR(10) primary key,--学生编号
  Sname VARCHAR(10),--姓名
  Sage DATETIME,--出生年月日
  Ssex CHAR(2) CHECK(Ssex='男' or Ssex='女')--性别
)
CREATE TABLE Teacher  --教师表
(
  TId VARCHAR(10) primary key,--教师编号
  Tname VARCHAR(10)--教师姓名
)  
CREATE TABLE Course --课程表
(
  CId VARCHAR(10) primary key,--课程编号
  Cname VARCHAR(10),--课程名称
  TId VARCHAR(10)--教师编号
)
CREATE TABLE SC--成绩表
(
  SId VARCHAR(10),--学生编号
  CId VARCHAR(10),--课程编号
  score INT NOT NULL CHECK(score<=100 AND score>=0)--成绩
)
INSERT INTO dbo.Teacher VALUES
('01' , '张三'),('02','李四'),('03','王五')
INSERT INTO dbo.Course VALUES
('01','语文','02'),('02','数学','01'),('03','英语','03')
INSERT INTO dbo.Student
SELECT'01', '赵雷','1990-01-01','男'UNION
SELECT'02','钱电','1990-12-21 ','男'UNION
SELECT'03','孙风','1990-12-20 ','男'UNION
SELECT'04','李云','1990-12-06 ','男'UNION
SELECT'05','周梅','1991-12-01 ','女'UNION
SELECT'06','吴兰','1992-01-01 ','女'UNION
SELECT'07','郑竹','1989-01-01 ','女'UNION
SELECT'08','张散','2017-12-20 ','女'UNION
SELECT'09','李四','2017-12-25 ','女'UNION
SELECT'10','唐震爹','2000-06-06 ','女'UNION
SELECT'11','赵柳','2013-03-13 ','女'UNION
SELECT'12','孙琪','2014-06-01 ','女'
INSERT INTO dbo.SC VALUES
('01','01',80),('01','02',90),('01','03',99),('02','01',70),('02','02',60),('02','03',80),
('03','01',80),('03','02',80),('03','03',80),('04','01',50),('04','02',30),('04','03',20),
('05','01',76),('05','02',87),('06','01',31),('06','03',34),('07','02',89),('07','03',98)
SELECT*FROM SC 
SELECT*FROM Student
SELECT*FROM Course
SELECT*FROM Teacher   

GO
--建立外键  
--课程表教师ID与教师表ID外键
ALTER TABLE Course
ADD CONSTRAINT TId_Teacher FOREIGN KEY (TId) REFERENCES Teacher (TId)

--成绩表TId与学生表TId链接
ALTER TABLE SC
ADD CONSTRAINT St_SC FOREIGN KEY (SId) REFERENCES Student (SId)

--成绩表课程CId与课程表CId链接
ALTER TABLE SC
ADD CONSTRAINT KC_SC FOREIGN KEY (CId) REFERENCES Course (CId)


GO
--练习题
--1.查询" 01 "课程比" 02 "课程成绩高的学生的信息及课程分数
--right join 右联接 返回包括右表中的所有记录和左表中联结字段相等的记录
--left join 左联接 返回包括左表中的所有记录和右表中联结字段相等的记录
SELECT * FROM Student RIGHT JOIN(SELECT t1.SId,class1,class2 FROM(SELECT SId,SCORE AS class1 FROM
SC WHERE SC.CId='01')AS t1,(SELECT SId,SCORE AS class2 FROM SC WHERE SC.CId='02')AS t2 WHERE t1.SId
=t2.SId AND t1.class1>t2.class2)t ON Student.SId=t.SId
--2.查询同时存在" 01 "课程和" 02 "课程的情况
SELECT * FROM (SELECT*FROM SC WHERE SC.CId='01')AS t1,(SELECT * FROM SC WHERE SC.CId='02')AS t2 
WHERE t1.SId=t2.SId
--3.查询存在" 01 "课程但可能不存在" 02 "课程的情况(不存在时显示为 null )
SELECT * FROM (SELECT * FROM SC WHERE SC.CId='01')AS T1 LEFT JOIN(SELECT * FROM SC WHERE SC.CId='02'
)AS T2 ON T1.SId=T2.SId
--4.查询不存在" 01 "课程但存在" 02 "课程的情况
--使用group by 子句对数据进行分组
--having 子句去掉不符合条件的组
--AVG 函数返回数值列的平均值。NULL 值不包括在计算中。
SELECT * FROM SC WHERE SC.SId NOT IN( SELECT SId FROM SC WHERE SC.CId='01')AND SC.CId='02'
--5.查询平均成绩大于等于 60 分的同学的学生编号和学生姓名和平均成绩
SELECT Student.SId,Sname,JG FROM Student,(SELECT SId,AVG(score)AS JG FROM SC GROUP BY SId HAVING
 AVG(score)>60)R WHERE Student.SId=R.SId;
 --6.查询在 SC 表存在成绩的学生信息
 -- DISTINCT 过滤结果集中的重复值
 SELECT DISTINCT Student.*FROM Student,SC WHERE Student.SId=SC.SId;
 --7.查询所有同学的学生编号、学生姓名、选课总数、所有课程的总成绩(没成绩的显示为 null )
 --SUM() 函数返回数值列的总数
 --COUNT 是统计满足条件的数
 SELECT s.SId, s.Sname,r.Coursenumber,r.scoresum FROM 
 ( (SELECT Student.SId,Student.Sname FROM Student )s Left JOIN (SELECT SC.SId, SUM(SC.score)
  AS scoresum, COUNT(SC.CId) AS Coursenumber FROM SC GROUP BY SC.sid )r ON s.SId = r.SId )
  --8 查有成绩的学生信息
  SELECT * FROM Student WHERE EXISTS(SELECT SC.SId FROM SC WHERE Student.SId=SC.SId)
  --9 查询「李」姓老师的数量
  SELECT  COUNT(*) FROM Teacher WHERE Tname LIKE '李%'
  --10 查询学过「张三」老师授课的同学的信息
  SELECT Student.* FROM Student,Teacher,Course,SC WHERE Student.SId=SC.SId AND Course.CId=SC.CId
  AND Course.TId=Teacher.TId AND Tname='张三'
  --11.查询没有学全所有课程的同学的信息
  --select * from student where student.sid not in ( select sc.sid from sc group by sc.sid having
   --count(sc.cid)= (select count(cid) from course) )
  --12 查询至少有一门课与学号为" 01 "的同学所学相同的同学的信息
  SELECT *FROM Student WHERE Student.SId IN (SELECT SC.SId FROM SC WHERE SC.CId IN(SELECT SC.CId
  FROM SC WHERE SC.SId='01'))
  --13 查询和" 01 "号的同学学习的课程 完全相同的其他同学的信息
  --14 查询没学过"张三"老师讲授的任一门课程的学生姓名
  SELECT * FROM Student WHERE Student.SId NOT IN 
  (SELECT SC.SId FROM SC WHERE SC.CId IN 
  (SELECT Course.CId FROM Course WHERE Course.TId IN 
  (SELECT Teacher.TId FROM Teacher WHERE Tname='张三')));
  --15 查询两门及其以上不及格课程的同学的学号，姓名及其平均成绩
  --SELECT Student.SId,Student.Sname,AVG(SC.score)FROM Student,SC WHERE Student.SId=SC.SId AND 
  --SC.score<60 GROUP BY SC.SId HAVING COUNT(*)>1
  --16 检索" 01 "课程分数小于 60，按分数降序排列的学生信息
  --排序语法为 ORDER BY ***** DESC\ASC
  SELECT Student.*,SC.score FROM Student,SC WHERE Student.SId=SC.SId AND SC.score<60 AND CId='01'
  ORDER BY SC.score DESC
  --17 按平均成绩从高到低显示所有学生的所有课程的成绩以及平均成绩
  --ORDER BY 语句用于对结果集进行排序。
  --DESC 降序
  SELECT * FROM SC Left JOIN(SELECT SId,AVG(SCORE)AS AVSCORE FROM SC GROUP BY SId)R ON SC.SId=
  R.SId ORDER BY AVSCORE DESC
  --18 查询各科成绩最高分、最低分和平均分：
  SELECT SC.CId,MAX(SC.score)AS 最高分,MIN(SC.score)AS 最低分,
  AVG(SC.score)AS 平均分 FROM SC GROUP BY SC.CId
  --19 按各科成绩进行排序，并显示排名， Score 重复时保留名次空缺
  SELECT a.CId, a.SId, a.score, COUNT(b.score)+1 AS RANK FROM SC AS a LEFT JOIN SC AS b ON 
  a.score<b.score and a.CId = b.CId GROUP BY a.CId, a.SId,a.score ORDER BY a.CId, RANK ASC
  --SELECT*,RANK()OVER (ORDER BY SCORE DESC)排名 FROM SC 
  --20 按各科成绩进行排序，并显示排名， Score 重复时合并名次
  --SELECT*,DENSE_RANK() OVER(ORDER BY SCORE DESC)排名 FROM SC
  --21 查询学生的总成绩，并进行排名，总分重复时保留名次空缺
    --SELECT *,RANK()OVER(ORDER BY 总成绩 DESC)排名 FROM (select SId,SUM(score)总成绩 
	--FROM SC GROUP BY SId)A; 
  --22 查询学生的总成绩，并进行排名，总分重复时不保留名次空缺
    --SELECT *,DENSE_RANK()OVER(ORDER BY 总成绩 DESC)排名 FROM(SELECT SId,SUM(score)总成绩
	--FROM SC GROUP BY SId)A;
  --23 统计各科成绩各分数段人数：课程编号，课程名称，[100-85]，[85-70]，[70-60]，[60-0] 及所占百分比
  --24 查询各科成绩前三名的记录
  SELECT * FROM SC a LEFT JOIN SC b ON a.CId = b.CId AND a.score<b.score ORDER BY a.cid,a.score
  --25 查询每门课程被选修的学生数
  SELECT CId, COUNT(SId) FROM SC GROUP BY CId
  --26 查询出只选修两门课程的学生学号和姓名
  SELECT Student.SId,Student.Sname FROM Student WHERE Student.SId IN(SELECT SC.SId FROM SC GROUP
   BY SC.SId HAVING COUNT(SC.CId)=2)
  --27 查询男生、女生人数
  SELECT Ssex,COUNT(*)FROM Student GROUP BY Ssex
  --28 查询名字中含有「风」字的学生信息
  SELECT*FROM Student WHERE Student.Sname LIKE'%风%'
  --29 查询同名同性学生名单，并统计同名人数
  --没有同名的
  --30 查询 1990 年出生的学生名单
  --YEAR函数返回指定日期的年的部分
  SELECT*FROM Student WHERE YEAR(Student.Sage)=1990
  --31 查询每门课程的平均成绩，结果按平均成绩降序排列，平均成绩相同时，按课程编号升序排列
  SELECT CID ,AVG(score)平均成绩 FROM SC GROUP BY CID ORDER BY 平均成绩 DESC
  --32. 查询平均成绩大于等于 85 的所有学生的学号、姓名和平均成绩 有问题
 -- SELECT S.SID, S.Sname, T.平均成绩 FROM Student S,(SELECT SC.SId,AVG(SC.score) 平均成绩 FROM SC 
 -- GROUP BY SC.SId HAVING  平均成绩 > 85)T WHERE S.SId=T.SId
  --33. 查询课程名称为「数学」，且分数低于 60 的学生姓名和分数
  SELECT S.Sname, SC.score FROM Student S, SC, Course C WHERE S.SId=SC.SId AND SC.score<60 AND
  SC.CId=C.CId AND C.Cname='数学'
  --34 查询所有学生的课程及分数情况（存在学生没成绩，没选课的情况）
  SELECT S.SId,SC.CId,SC.score FROM Student S LEFT JOIN SC 
  ON S.SId=SC.SId;
  --35 查询任何一门课程成绩在 70 分以上的姓名、课程名称和分数
  SELECT S.Sname,C.Cname,SC.score FROM Student S,Course C ,SC WHERE SC.score>
  =70 AND S.SId=SC.SId AND SC.CId=C.CId;
  --36 查询不及格的课程
  SELECT DISTINCT CId FROM SC WHERE score<60
  --37 查询课程编号为 01 且课程成绩在 80 分以上的学生的学号和姓名
  SELECT SC.SId,S.Sname FROM Student S,SC WHERE SC.CId='01' AND
  S.SId=SC.SId AND SC.score>80 
  --38. 求每门课程的学生人数
  SELECT CId,COUNT(SId) 人数 FROM SC GROUP BY CId
  --39 成绩不重复，查询选修「张三」老师所授课程的学生中，成绩最高的学生信息及其成绩
  --(LIMIT N)提取前N条记录
  --(LIMIT POS N)选取从POS开始后的N条记录
  SELECT S.*,SC.score FROM Student S,Course C,Teacher T ,SC WHERE
  C.CId=SC.CId AND C.TId=T.TId AND T.Tname='张三'AND S.SId=SC.SId
  ORDER BY SC.score DESC LIMIT 1
  --40 成绩有重复的情况下，查询选修「张三」老师所授课程的学生中，成绩最高的学生信息及其成绩
  SELECT * FROM (SELECT SC.SId,DENSE_RANK() OVER(PARTITION BY SC.CId ORDER BY SC.SCORE DESC)排名 FROM SC
  WHERE SC.CId IN (SELECT C.CId FROM Course C,Teacher T WHERE C.TId=T.TId AND
  T.Tname='张三'))T1 WHERE T1.排名=1
  --41 查询不同课程成绩相同的学生的学生编号、课程编号、学生成绩
  SELECT * FROM SC T1 WHERE EXISTS(SELECT * FROM SC T2 WHERE T1.SId=T2.SId AND T1.CId!=T2.CId AND T1.score=T2.score)
  --42 查询每门功成绩最好的前两名
  SELECT S.*FROM Student S INNER JOIN (SELECT SC.SId,ROW_NUMBER()OVER (PARTITION BY SC.CId ORDER BY SC.score)排名 FROM SC)
  T WHERE T.排名<3 AND S.SId=T.SId
  --43. 统计每门课程的学生选修人数（超过 5 人的课程才统计）
  SELECT CId,COUNT(SId)超过五人的学科 FROM SC GROUP BY CId HAVING COUNT(SId)>5
  --44. 检索至少选修两门课程的学生学号
  SELECT SId FROM SC GROUP BY SId HAVING COUNT(CId)>2
  --45 查询选修了全部课程的学生信息
  SELECT S.*FROM Student S WHERE S.SId IN (SELECT SId FROM SC GROUP BY SId HAVING COUNT(CId)=3)
  --46 查询各学生的年龄，只按年份来算
  --NOW() 函数返回当前系统的日期和时间。
  --FRAC_SECOND。表示间隔是毫秒
  --SECOND。秒
  -- MINUTE。分钟
  -- HOUR。小时
  -- DAY。天
  -- WEEK。星期
  --MONTH。月
  --QUARTER。季度
  --YEAR。年
  --SELECT SId,Sname,TIMESTAMPDIFF(YEAR,Sage,NOW())AS 年龄 FROM Student 
  --47 按照出生日期来算，当前月日 < 出生年月的月日则，年龄减一
  --SELECT SId,Sname,((DATE_FORMAT(NOW(),'%Y')-DATE_FORMAT(Sage,'%Y')-(CASE WHEN DATE_FORMAT(NOW(),'%m%d')>DATE_FORMAT(Sage,'%m%d') THEN 0 ELSE 1 END)) 年龄 
  --FROM Student
  --48 查询本周过生日的学生 
  --WEEK(date[,mode]) 此函数返回日期的周数
  SELECT * FROM Student WHERE WEEK(Sage)=WEEK(NOW())
  --49 查询下周过生日的学生
  SELECT * FROM Student WHERE WEEK(Sage)=WEEK(NOW())+1
  --50 查询本月过生日的学生
  SELECT * FROM Student WHERE MONTH(Sage)=MONTH(NOW())
  --51 查询下月过生日的学生
  SELECT * FROM Student WHERE MONTH(Sage)=MONTH(NOW())+1
  --查询出第5到第10名同学的基本信息，ID不连续