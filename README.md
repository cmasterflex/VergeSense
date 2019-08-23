#Missing Data
I noticed that some of the sensor readings were missing some fields.
For this exercise I chose to exclude all such data points.

##Missing Sensor Number
I could see a system being implemented to try and infer the missing sensor number, say if sensor 2 was reporting a person every 10 minutes and then at one of those intervals a report is made with no sensor number, but then after that sensor 2 reports again, it could be implied that sensor 2 reported the unknown data point. I guess it depends on whether you value more data, or more acurate data.

##Missing Time Stamp
As similar argument could be made for missing time stamps, they could potentially be inferred by surrounding data, if that was a priority. 


#Bad Data
I noticed that at one point Sensor 4 reported 99999999999999, which is obviously an error, I decided to put a cap of 100 people as a valid person count, as that seemd like more than enough to cover even a vary large conference room. For this exrecise I chose to exclude any data that went over that max person count, but similarily to missing data, one could also infer a reasonable count based on surounding data points, if desired. 
