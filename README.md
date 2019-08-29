# Overview
For this exercise I decided to use .Net Core for my back-end, and React for my front-end. I chose .Net Core because I have never worked specifically with it before, but I am familiar with .Net Framework, and .Net Core is the future of .Net, so I wanted to learn it. I chose React for the front-end because 1) it was suggeted by the exercise, and 2) I wanted to learn it also, since I have never used React before.

I created a basic API project in Visual Studio using the 'React' template. This template creates a basic React app in a folder called 'ClientApp' using a slightly modified version of the standard 'create-react-app' command. 
In order to duplicate the functionality of the supplied codepen in my app, I ran the following commands in the 'ClientApp' folder:
````
> npm install react-datetime --save
> npm install react-vis --save
> npm install moment --save
````
I then added the required css files to my index.html:
````
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
<link rel="stylesheet" href="https://unpkg.com/react-vis/dist/style.css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/react-datetime/3.0.0/css/react-datetime.min.css" />
````


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

# Part 2: Sensor Data API
For this application I split the bulk of logic into two services; the CsvService and the SensorService.

## CsvService
The `CsvService` implements the `ICsvService` interface which has one method: `LoadFile()`.
`LoadFile()` reads the contents of a file at a hardcoded file path (I hardcoded the path for this example, but normally this would be stored in a config file) line by line, and then adds each line to a `List<string>`.
After all lines have been read, `LoadFile()` then runs parallel `for` loops to parse each line into a `SensorData` object.
`LoadFile()` returns and `IEnumerable<SensorData>`.

### Sensor Data Object
The `SensorData` object represents the parsed values of one line of the csv file, and it has 3 properties: `string Id`, `DateTime TimeStamp`, and `int PersonCount`. All of these properties are read-only, and can only be set via constuctor, this is to prevent data being changed once a data point has been created.
The `SensorData` object also has one static method: `bool TryParseSensorData(string rawData, out SensorData sensorData)`, which takes in one line of the csv file as a `string`, and parses it into a `SensorData` object, it returns true if the line could be successfully parsed, and false if it could not be.

### CSV Parsing Decisions
#### Missing Data
I noticed that some of the sensor readings were missing some fields, and for this exercise I chose to exclude all such data points.

##### Missing Sensor Number
I could see a system being implemented to try and infer the missing sensor number, say if sensor 2 was reporting a person every 10 minutes and then at one of those intervals a report is made with no sensor number, but then after that sensor 2 reports again, it could be implied that sensor 2 reported the unknown data point. I guess it depends on whether you value more data, or more acurate data.

##### Missing Time Stamp
As similar argument could be made for missing time stamps, they could potentially be inferred by surrounding data, if that was a priority. 

#### Bad Data
I noticed that at one point Sensor 4 reported 99999999999999, which is obviously an error, I decided to put a cap of 100 people as a valid person count, as that seemd like more than enough to cover even a vary large conference room. For this exrecise I chose to exclude any data that went over that max person count, but similarily to missing data, one could also infer a reasonable count based on surounding data points, if desired.

## SensorService
The `SensorService` implements the `ISensorService` interface, and is injected with an `ICsvService` on creation. The `SensorService` has one method `GetData(DateTime start, DateTime end)`, which when called, loads all sensor data from the `CsvService`'s `LoadFile()` method, caches that data for future calls to the service (since the file is always the same), and then builds a return object which is then serialized in to JSON and returned by the `SensorController` API.

I decided to orginize the JSON object like this:
````
[
    {
        "id": "Sensor 1",
        "max": 4,
        "data": [
            {
                "timeStamp": "2019-03-07T19:09:24Z",
                "personCount": 0
            },
            ...           
        ]
    },
    {
        "id": "Sensor 2",
        "max": 10,
        "data": [ ... ]
    },
    {
        "id": "Sensor 3",
        "max": 18,
        "data": [ ... ]
    }
]
````
I grouped all data first by 'sensor id', and then for each sensor I return the max person count, and an array of data points. Each data point is made up of a time stamp, and a person count. Sensor data is sorted by date, and then filtered by 'start' and 'end' date. If no start or end date are given, start will default to `DateTime.MinValue`, and end will dafault to `DateTime.Now`.

### Max Person Count
I debated whether to filter the max person count by the date range, but ultimatly decided not to, as it wasn't specified in the requirements.

#### Max Sensor Readings (excluding erroneous data):
**Sensor 1:** 4

**Sensor 2:** 10

**Sensor 3:** 18

# Part 3: User Interface
![userInterface](/UserInterface.jpg)

For my own ease of understanding the data, I decided to separate each sensor out into it's own chart, in addition to the combined chart. All four charts update when a start or and time is selected.  If a given sensor has no data for the selected timespan, then that sensor's chart will not be displayed.
