CREATE DATABASE InLettLX
ON PRIMARY
(    
    NAME='InLettLX',
	FILENAME='D:\SQL\0310��ϰ\InLettLX.mdf',
	SIZE=10MB,
	FILEGROWTH=10%
)
LOG ON
(
   NAME='InLettLX_log',
   FILENAME='D:\SQL\0310��ϰ\InLettLX_log.ldf',
   SIZE=10MB,
   FILEGROWTH=10%
)
USE InLettLX
GO
CREATE TABLE Student --ѧ����
(
  SId VARCHAR(10) primary key,--ѧ�����
  Sname VARCHAR(10),--����
  Sage DATETIME,--����������
  Ssex CHAR(2) CHECK(Ssex='��' or Ssex='Ů')--�Ա�
)
CREATE TABLE Teacher  --��ʦ��
(
  TId VARCHAR(10) primary key,--��ʦ���
  Tname VARCHAR(10)--��ʦ����
)  
CREATE TABLE Course --�γ̱�
(
  CId VARCHAR(10) primary key,--�γ̱��
  Cname VARCHAR(10),--�γ�����
  TId VARCHAR(10)--��ʦ���
)
CREATE TABLE SC--�ɼ���
(
  SId VARCHAR(10),--ѧ�����
  CId VARCHAR(10),--�γ̱��
  score INT NOT NULL CHECK(score<=100 AND score>=0)--�ɼ�
)
INSERT INTO dbo.Teacher VALUES
('01' , '����'),('02','����'),('03','����')
INSERT INTO dbo.Course VALUES
('01','����','02'),('02','��ѧ','01'),('03','Ӣ��','03')
INSERT INTO dbo.Student
SELECT'01', '����','1990-01-01','��'UNION
SELECT'02','Ǯ��','1990-12-21 ','��'UNION
SELECT'03','���','1990-12-20 ','��'UNION
SELECT'04','����','1990-12-06 ','��'UNION
SELECT'05','��÷','1991-12-01 ','Ů'UNION
SELECT'06','����','1992-01-01 ','Ů'UNION
SELECT'07','֣��','1989-01-01 ','Ů'UNION
SELECT'08','��ɢ','2017-12-20 ','Ů'UNION
SELECT'09','����','2017-12-25 ','Ů'UNION
SELECT'10','�����','2000-06-06 ','Ů'UNION
SELECT'11','����','2013-03-13 ','Ů'UNION
SELECT'12','����','2014-06-01 ','Ů'
INSERT INTO dbo.SC VALUES
('01','01',80),('01','02',90),('01','03',99),('02','01',70),('02','02',60),('02','03',80),
('03','01',80),('03','02',80),('03','03',80),('04','01',50),('04','02',30),('04','03',20),
('05','01',76),('05','02',87),('06','01',31),('06','03',34),('07','02',89),('07','03',98)
SELECT*FROM SC 
SELECT*FROM Student
SELECT*FROM Course
SELECT*FROM Teacher   

GO
--�������  
--�γ̱��ʦID���ʦ��ID���
ALTER TABLE Course
ADD CONSTRAINT TId_Teacher FOREIGN KEY (TId) REFERENCES Teacher (TId)

--�ɼ���TId��ѧ����TId����
ALTER TABLE SC
ADD CONSTRAINT St_SC FOREIGN KEY (SId) REFERENCES Student (SId)

--�ɼ���γ�CId��γ̱�CId����
ALTER TABLE SC
ADD CONSTRAINT KC_SC FOREIGN KEY (CId) REFERENCES Course (CId)


GO
--��ϰ��
--1.��ѯ" 01 "�γ̱�" 02 "�γ̳ɼ��ߵ�ѧ������Ϣ���γ̷���
--right join ������ ���ذ����ұ��е����м�¼������������ֶ���ȵļ�¼
--left join ������ ���ذ�������е����м�¼���ұ��������ֶ���ȵļ�¼
SELECT * FROM Student RIGHT JOIN(SELECT t1.SId,class1,class2 FROM(SELECT SId,SCORE AS class1 FROM
SC WHERE SC.CId='01')AS t1,(SELECT SId,SCORE AS class2 FROM SC WHERE SC.CId='02')AS t2 WHERE t1.SId
=t2.SId AND t1.class1>t2.class2)t ON Student.SId=t.SId
--2.��ѯͬʱ����" 01 "�γ̺�" 02 "�γ̵����
SELECT * FROM (SELECT*FROM SC WHERE SC.CId='01')AS t1,(SELECT * FROM SC WHERE SC.CId='02')AS t2 
WHERE t1.SId=t2.SId
--3.��ѯ����" 01 "�γ̵����ܲ�����" 02 "�γ̵����(������ʱ��ʾΪ null )
SELECT * FROM (SELECT * FROM SC WHERE SC.CId='01')AS T1 LEFT JOIN(SELECT * FROM SC WHERE SC.CId='02'
)AS T2 ON T1.SId=T2.SId
--4.��ѯ������" 01 "�γ̵�����" 02 "�γ̵����
--ʹ��group by �Ӿ�����ݽ��з���
--having �Ӿ�ȥ����������������
--AVG ����������ֵ�е�ƽ��ֵ��NULL ֵ�������ڼ����С�
SELECT * FROM SC WHERE SC.SId NOT IN( SELECT SId FROM SC WHERE SC.CId='01')AND SC.CId='02'
--5.��ѯƽ���ɼ����ڵ��� 60 �ֵ�ͬѧ��ѧ����ź�ѧ��������ƽ���ɼ�
SELECT Student.SId,Sname,JG FROM Student,(SELECT SId,AVG(score)AS JG FROM SC GROUP BY SId HAVING
 AVG(score)>60)R WHERE Student.SId=R.SId;
 --6.��ѯ�� SC ����ڳɼ���ѧ����Ϣ
 -- DISTINCT ���˽�����е��ظ�ֵ
 SELECT DISTINCT Student.*FROM Student,SC WHERE Student.SId=SC.SId;
 --7.��ѯ����ͬѧ��ѧ����š�ѧ��������ѡ�����������пγ̵��ܳɼ�(û�ɼ�����ʾΪ null )
 --SUM() ����������ֵ�е�����
 --COUNT ��ͳ��������������
 SELECT s.SId, s.Sname,r.Coursenumber,r.scoresum FROM 
 ( (SELECT Student.SId,Student.Sname FROM Student )s Left JOIN (SELECT SC.SId, SUM(SC.score)
  AS scoresum, COUNT(SC.CId) AS Coursenumber FROM SC GROUP BY SC.sid )r ON s.SId = r.SId )
  --8 ���гɼ���ѧ����Ϣ
  SELECT * FROM Student WHERE EXISTS(SELECT SC.SId FROM SC WHERE Student.SId=SC.SId)
  --9 ��ѯ�������ʦ������
  SELECT  COUNT(*) FROM Teacher WHERE Tname LIKE '��%'
  --10 ��ѯѧ������������ʦ�ڿε�ͬѧ����Ϣ
  SELECT Student.* FROM Student,Teacher,Course,SC WHERE Student.SId=SC.SId AND Course.CId=SC.CId
  AND Course.TId=Teacher.TId AND Tname='����'
  --11.��ѯû��ѧȫ���пγ̵�ͬѧ����Ϣ
  --select * from student where student.sid not in ( select sc.sid from sc group by sc.sid having
   --count(sc.cid)= (select count(cid) from course) )
  --12 ��ѯ������һ�ſ���ѧ��Ϊ" 01 "��ͬѧ��ѧ��ͬ��ͬѧ����Ϣ
  SELECT *FROM Student WHERE Student.SId IN (SELECT SC.SId FROM SC WHERE SC.CId IN(SELECT SC.CId
  FROM SC WHERE SC.SId='01'))
  --13 ��ѯ��" 01 "�ŵ�ͬѧѧϰ�Ŀγ� ��ȫ��ͬ������ͬѧ����Ϣ
  --14 ��ѯûѧ��"����"��ʦ���ڵ���һ�ſγ̵�ѧ������
  SELECT * FROM Student WHERE Student.SId NOT IN 
  (SELECT SC.SId FROM SC WHERE SC.CId IN 
  (SELECT Course.CId FROM Course WHERE Course.TId IN 
  (SELECT Teacher.TId FROM Teacher WHERE Tname='����')));
  --15 ��ѯ���ż������ϲ�����γ̵�ͬѧ��ѧ�ţ���������ƽ���ɼ�
  --SELECT Student.SId,Student.Sname,AVG(SC.score)FROM Student,SC WHERE Student.SId=SC.SId AND 
  --SC.score<60 GROUP BY SC.SId HAVING COUNT(*)>1
  --16 ����" 01 "�γ̷���С�� 60���������������е�ѧ����Ϣ
  --�����﷨Ϊ ORDER BY ***** DESC\ASC
  SELECT Student.*,SC.score FROM Student,SC WHERE Student.SId=SC.SId AND SC.score<60 AND CId='01'
  ORDER BY SC.score DESC
  --17 ��ƽ���ɼ��Ӹߵ�����ʾ����ѧ�������пγ̵ĳɼ��Լ�ƽ���ɼ�
  --ORDER BY ������ڶԽ������������
  --DESC ����
  SELECT * FROM SC Left JOIN(SELECT SId,AVG(SCORE)AS AVSCORE FROM SC GROUP BY SId)R ON SC.SId=
  R.SId ORDER BY AVSCORE DESC
  --18 ��ѯ���Ƴɼ���߷֡���ͷֺ�ƽ���֣�
  SELECT SC.CId,MAX(SC.score)AS ��߷�,MIN(SC.score)AS ��ͷ�,
  AVG(SC.score)AS ƽ���� FROM SC GROUP BY SC.CId
  --19 �����Ƴɼ��������򣬲���ʾ������ Score �ظ�ʱ�������ο�ȱ
  SELECT a.CId, a.SId, a.score, COUNT(b.score)+1 AS RANK FROM SC AS a LEFT JOIN SC AS b ON 
  a.score<b.score and a.CId = b.CId GROUP BY a.CId, a.SId,a.score ORDER BY a.CId, RANK ASC
  --SELECT*,RANK()OVER (ORDER BY SCORE DESC)���� FROM SC 
  --20 �����Ƴɼ��������򣬲���ʾ������ Score �ظ�ʱ�ϲ�����
  --SELECT*,DENSE_RANK() OVER(ORDER BY SCORE DESC)���� FROM SC
  --21 ��ѯѧ�����ܳɼ����������������ܷ��ظ�ʱ�������ο�ȱ
    --SELECT *,RANK()OVER(ORDER BY �ܳɼ� DESC)���� FROM (select SId,SUM(score)�ܳɼ� 
	--FROM SC GROUP BY SId)A; 
  --22 ��ѯѧ�����ܳɼ����������������ܷ��ظ�ʱ���������ο�ȱ
    --SELECT *,DENSE_RANK()OVER(ORDER BY �ܳɼ� DESC)���� FROM(SELECT SId,SUM(score)�ܳɼ�
	--FROM SC GROUP BY SId)A;
  --23 ͳ�Ƹ��Ƴɼ����������������γ̱�ţ��γ����ƣ�[100-85]��[85-70]��[70-60]��[60-0] ����ռ�ٷֱ�
  --24 ��ѯ���Ƴɼ�ǰ�����ļ�¼
  SELECT * FROM SC a LEFT JOIN SC b ON a.CId = b.CId AND a.score<b.score ORDER BY a.cid,a.score
  --25 ��ѯÿ�ſγ̱�ѡ�޵�ѧ����
  SELECT CId, COUNT(SId) FROM SC GROUP BY CId
  --26 ��ѯ��ֻѡ�����ſγ̵�ѧ��ѧ�ź�����
  SELECT Student.SId,Student.Sname FROM Student WHERE Student.SId IN(SELECT SC.SId FROM SC GROUP
   BY SC.SId HAVING COUNT(SC.CId)=2)
  --27 ��ѯ������Ů������
  SELECT Ssex,COUNT(*)FROM Student GROUP BY Ssex
  --28 ��ѯ�����к��С��硹�ֵ�ѧ����Ϣ
  SELECT*FROM Student WHERE Student.Sname LIKE'%��%'
  --29 ��ѯͬ��ͬ��ѧ����������ͳ��ͬ������
  --û��ͬ����
  --30 ��ѯ 1990 �������ѧ������
  --YEAR��������ָ�����ڵ���Ĳ���
  SELECT*FROM Student WHERE YEAR(Student.Sage)=1990
  --31 ��ѯÿ�ſγ̵�ƽ���ɼ��������ƽ���ɼ��������У�ƽ���ɼ���ͬʱ�����γ̱����������
  SELECT CID ,AVG(score)ƽ���ɼ� FROM SC GROUP BY CID ORDER BY ƽ���ɼ� DESC
  --32. ��ѯƽ���ɼ����ڵ��� 85 ������ѧ����ѧ�š�������ƽ���ɼ� ������
 -- SELECT S.SID, S.Sname, T.ƽ���ɼ� FROM Student S,(SELECT SC.SId,AVG(SC.score) ƽ���ɼ� FROM SC 
 -- GROUP BY SC.SId HAVING  ƽ���ɼ� > 85)T WHERE S.SId=T.SId
  --33. ��ѯ�γ�����Ϊ����ѧ�����ҷ������� 60 ��ѧ�������ͷ���
  SELECT S.Sname, SC.score FROM Student S, SC, Course C WHERE S.SId=SC.SId AND SC.score<60 AND
  SC.CId=C.CId AND C.Cname='��ѧ'
  --34 ��ѯ����ѧ���Ŀγ̼��������������ѧ��û�ɼ���ûѡ�ε������
  SELECT S.SId,SC.CId,SC.score FROM Student S LEFT JOIN SC 
  ON S.SId=SC.SId;
  --35 ��ѯ�κ�һ�ſγ̳ɼ��� 70 �����ϵ��������γ����ƺͷ���
  SELECT S.Sname,C.Cname,SC.score FROM Student S,Course C ,SC WHERE SC.score>
  =70 AND S.SId=SC.SId AND SC.CId=C.CId;
  --36 ��ѯ������Ŀγ�
  SELECT DISTINCT CId FROM SC WHERE score<60
  --37 ��ѯ�γ̱��Ϊ 01 �ҿγ̳ɼ��� 80 �����ϵ�ѧ����ѧ�ź�����
  SELECT SC.SId,S.Sname FROM Student S,SC WHERE SC.CId='01' AND
  S.SId=SC.SId AND SC.score>80 
  --38. ��ÿ�ſγ̵�ѧ������
  SELECT CId,COUNT(SId) ���� FROM SC GROUP BY CId
  --39 �ɼ����ظ�����ѯѡ�ޡ���������ʦ���ڿγ̵�ѧ���У��ɼ���ߵ�ѧ����Ϣ����ɼ�
  --(LIMIT N)��ȡǰN����¼
  --(LIMIT POS N)ѡȡ��POS��ʼ���N����¼
  SELECT S.*,SC.score FROM Student S,Course C,Teacher T ,SC WHERE
  C.CId=SC.CId AND C.TId=T.TId AND T.Tname='����'AND S.SId=SC.SId
  ORDER BY SC.score DESC LIMIT 1
  --40 �ɼ����ظ�������£���ѯѡ�ޡ���������ʦ���ڿγ̵�ѧ���У��ɼ���ߵ�ѧ����Ϣ����ɼ�
  SELECT * FROM (SELECT SC.SId,DENSE_RANK() OVER(PARTITION BY SC.CId ORDER BY SC.SCORE DESC)���� FROM SC
  WHERE SC.CId IN (SELECT C.CId FROM Course C,Teacher T WHERE C.TId=T.TId AND
  T.Tname='����'))T1 WHERE T1.����=1
  --41 ��ѯ��ͬ�γ̳ɼ���ͬ��ѧ����ѧ����š��γ̱�š�ѧ���ɼ�
  SELECT * FROM SC T1 WHERE EXISTS(SELECT * FROM SC T2 WHERE T1.SId=T2.SId AND T1.CId!=T2.CId AND T1.score=T2.score)
  --42 ��ѯÿ�Ź��ɼ���õ�ǰ����
  SELECT S.*FROM Student S INNER JOIN (SELECT SC.SId,ROW_NUMBER()OVER (PARTITION BY SC.CId ORDER BY SC.score)���� FROM SC)
  T WHERE T.����<3 AND S.SId=T.SId
  --43. ͳ��ÿ�ſγ̵�ѧ��ѡ������������ 5 �˵Ŀγ̲�ͳ�ƣ�
  SELECT CId,COUNT(SId)�������˵�ѧ�� FROM SC GROUP BY CId HAVING COUNT(SId)>5
  --44. ��������ѡ�����ſγ̵�ѧ��ѧ��
  SELECT SId FROM SC GROUP BY SId HAVING COUNT(CId)>2
  --45 ��ѯѡ����ȫ���γ̵�ѧ����Ϣ
  SELECT S.*FROM Student S WHERE S.SId IN (SELECT SId FROM SC GROUP BY SId HAVING COUNT(CId)=3)
  --46 ��ѯ��ѧ�������䣬ֻ���������
  --NOW() �������ص�ǰϵͳ�����ں�ʱ�䡣
  --FRAC_SECOND����ʾ����Ǻ���
  --SECOND����
  -- MINUTE������
  -- HOUR��Сʱ
  -- DAY����
  -- WEEK������
  --MONTH����
  --QUARTER������
  --YEAR����
  --SELECT SId,Sname,TIMESTAMPDIFF(YEAR,Sage,NOW())AS ���� FROM Student 
  --47 ���ճ����������㣬��ǰ���� < �������µ������������һ
  --SELECT SId,Sname,((DATE_FORMAT(NOW(),'%Y')-DATE_FORMAT(Sage,'%Y')-(CASE WHEN DATE_FORMAT(NOW(),'%m%d')>DATE_FORMAT(Sage,'%m%d') THEN 0 ELSE 1 END)) ���� 
  --FROM Student
  --48 ��ѯ���ܹ����յ�ѧ�� 
  --WEEK(date[,mode]) �˺����������ڵ�����
  SELECT * FROM Student WHERE WEEK(Sage)=WEEK(NOW())
  --49 ��ѯ���ܹ����յ�ѧ��
  SELECT * FROM Student WHERE WEEK(Sage)=WEEK(NOW())+1
  --50 ��ѯ���¹����յ�ѧ��
  SELECT * FROM Student WHERE MONTH(Sage)=MONTH(NOW())
  --51 ��ѯ���¹����յ�ѧ��
  SELECT * FROM Student WHERE MONTH(Sage)=MONTH(NOW())+1
  --��ѯ����5����10��ͬѧ�Ļ�����Ϣ��ID������