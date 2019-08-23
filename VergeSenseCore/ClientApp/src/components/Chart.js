import React, { Component } from 'react';
import { XYPlot, XAxis, YAxis, HorizontalGridLines, VerticalGridLines, LineSeries } from 'react-vis';
import Datetime from 'react-datetime';

const API = 'api/Sensor/SensorData';

export class ChartComponent extends Component {
    // @INFO: axios, reactVis, moment, Datetime and libraries have already been included...

    constructor(props) {
        super(props);

        this.state = {
            sensors: [],
            //sensors: [{
            //    sensorData: []
            //}],
            loading: false,
            start: null,
            end: null
        };

        fetch(API)
            .then(response => response.json())
            .then(data => {
                this.setState({ sensors: data, loading: false });
            });

    }

    handleDateChange() {
        this.setState({loading: true});
        fetch(API)
            .then(response => response.json())
            .then(data => {
                this.setState({ sensorData: data, loading: false });
            });
    }

    handleStartChange(startDate) {
        this.setState({ start: startDate });
        this.handleDateChange();
    }

    handleEndChange(endDate) {
        this.setState({ end: endDate });
        this.handleDateChange();
    }

    renderSensors() {
        return (
            this.state.sensors.map(sensor =>
                    <LineSeries data={sensor.data.map(dataPoint => ({ x: new Date(dataPoint.timeStamp), y: dataPoint.personCount }))} />)
        );
    }

    render() {
        let sensorPlot = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderSensors(this.state.sensors);


        return (
            <div>
                <div>
                    {this.state.sensors.map(sensor => <p>{sensor.datapoint}</p>/*{sensor.dataPoint.id}: {dataPoint.personCount} people at {dataPoint.timeStamp}</p>)*/)}
                </div>
                <div id="start-date">
                    <Datetime
                        onBlur={(startDate) => this.handleStartChange(startDate)}
                        inputProps={{ placeholder: 'select start date' }}
                        closeOnSelect
                    />
                </div>
                <div id="end-date">
                    <Datetime
                        onBlur={(endDate) => this.handleEndChange(endDate)}
                        inputProps={{ placeholder: 'select end date' }}
                        closeOnSelect
                    />
                </div>
                {this.state.loading
                    ? <p><em>Loading...</em></p>
                    : <XYPlot
                        xType="time"
                        height={300}
                        width={900}
                      >
                        <VerticalGridLines />
                        <HorizontalGridLines />
                        <XAxis />
                        <YAxis />
                        {sensorPlot}
                    </XYPlot>}
            </div>
        );
    }
}