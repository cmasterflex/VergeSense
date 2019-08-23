import React, { Component } from 'react';

export class FetchData extends Component {

    static renderSensorData(sensorData) {
        return (
            <div>
                {sensorData.map(dataPoint => <p>{dataPoint.id}: {dataPoint.personCount} people at {dataPoint.timeStamp}</p>)}
            </div>
        );
    }

  displayName = FetchData.name

  constructor(props) {
    super(props);
    this.state = { sensorData: [], loading: true };

    fetch('api/Sensor/SensorData')
      .then(response => response.json())
      .then(data => {
        this.setState({ sensorData: data, loading: false });
      });
  }


  render() {
    let contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : FetchData.renderSensorData(this.state.sensorData);

    return (
      <div>
        <h1>Raw Sensor Data</h1>
        {contents}
      </div>
    );
  }
}