# Part 1: Database Diagram
![databaseDiagram](/DatabaseDiagram.jpg)
### Readings Table
I decided that sensor data is the core concern for this database, so I made a Readings table to contain all sensor readings.
This table has a Primary Key of 'reading_id', a 'date' column for the dateTime when the sensor reports, and a 'person_count' column to record the person count at that time.
The Readings table also has a Foreign Key link to the Sensors table, so that we know what sensor the reading came from.
### Sensors Table
This table holds a list of all the sensors.
I added a 'name' column just in case a user wanted to name sensors to remember them easier.
The Sensors table also has a Foreign Key link to the Rooms table, since I assumed that a sensor can only be in one room at once.
### Rooms Table
This table holds a list of all the rooms.
This table also has a name column for convenience.
This table has a Foreign Key link to the Buildings table, since I assumed that a room can only be part of one building.
### Buildings Table
This table holds a list of all buildings.
I also added a name column here for convenience.
### Room - Zone Link Table
Since rooms can be placed in multiple zones, I added the Room_Zone table to keep track of these links.
### Zone Table
Last I created a Zone table to keep track of user defined zones.
I added a name column here as well.

# Missing Data
I noticed that some of the sensor readings were missing some fields.
For this exercise I chose to exclude all such data points.

### Missing Sensor Number
I could see a system being implemented to try and infer the missing sensor number, say if sensor 2 was reporting a person every 10 minutes and then at one of those intervals a report is made with no sensor number, but then after that sensor 2 reports again, it could be implied that sensor 2 reported the unknown data point. I guess it depends on whether you value more data, or more acurate data.

### Missing Time Stamp
As similar argument could be made for missing time stamps, they could potentially be inferred by surrounding data, if that was a priority. 


# Bad Data
I noticed that at one point Sensor 4 reported 99999999999999, which is obviously an error, I decided to put a cap of 100 people as a valid person count, as that seemd like more than enough to cover even a vary large conference room. For this exrecise I chose to exclude any data that went over that max person count, but similarily to missing data, one could also infer a reasonable count based on surounding data points, if desired. 
