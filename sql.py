import random

for month in range(1,6):
    day_min=1
    day_max=0
    if month==2:
        day_max=28
    elif month==1:
        day_min=15
        day_max=31
    elif month==5:
        day_max=15
    elif month % 2 == 1:
        day_max=31
    else:
        day_max=30
    for day in range(day_min,day_max+1):
        timeStamp=''
        time=str(random.randint(10,24))+":"+str(random.randint(10,60))+":"+str(random.randint(10,60))
        if day<10:
            timeStamp="2021-0"+str(month)+"-0"+str(day)+" "+time
        else:
            timeStamp="2021-0"+str(month)+"-"+str(day)+" "+time
        sql="INSERT INTO gz(channel,gzname,TimeStamp) VALUES('"+"主提故障"+"','"+"励磁柜风机无风"+"','"+timeStamp+"');"
        print(sql)
        timeStamp=''
        time=str(random.randint(10,24))+":"+str(random.randint(10,60))+":"+str(random.randint(10,60))
        if day<10:
            timeStamp="2021-0"+str(month)+"-0"+str(day)+" "+time
        else:
            timeStamp="2021-0"+str(month)+"-"+str(day)+" "+time
        sql="INSERT INTO gz(channel,gzname,TimeStamp) VALUES('"+"副提故障"+"','"+"励磁柜风机无风"+"','"+timeStamp+"');"
        print(sql)

# time='00:00:00'
# id=50000
# for month in range(1,6):
#     day_min=1
#     day_max=0
#     if month==2:
#         day_max=28
#     elif month==1:
#         day_min=15
#         day_max=31
#     elif month==5:
#         day_max=15
#     elif month % 2 == 1:
#         day_max=31
#     else:
#         day_max=30
#     for day in range(day_min,day_max+1):
#         timeStamp=''
#         if day<10:
#             timeStamp="2021-0"+str(month)+"-0"+str(day)+" "
#         else:
#             timeStamp="2021-0"+str(month)+"-"+str(day)+" "
        
#         for hour in range(0,25):
#             tmp=''
#             if hour<10:
#                 tmp=timeStamp+"0"+str(hour)+":00:00"
#             else:
#                 tmp=timeStamp+str(hour)+":00:00"
#             sql="INSERT INTO mzzyl_h(id,residual_pressure,normal_pressure,TimeStamp) VALUES('"+str(id)+"','"+str((-1)*random.randint(2000,2500)/10000)+"','"+str(0)+"','"+tmp+"');\n"
#             with open("./test.txt","a") as f:
#                 f.write(sql)
#             id=id+1