import React, { Component } from 'react';
import { XYPlot, XAxis, YAxis, HorizontalGridLines, VerticalGridLines, LineSeries } from 'react-vis';
import Datetime from 'react-datetime';

const API = 'api/Sensor/SensorData';

export class ChartComponent extends Component {
    // @INFO: axios, reactVis, moment, Datetime and libraries have already been included...

    constructor(props) {
        super(props);

        this.state = {
            sensorData: [],
            loading: false,
            start: null,
            end: null,
        };

        fetch(API)
            .then(response => response.json())
            .then(data => {
                this.setState({ sensorData: data, loading: false });
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

    render() {
        return (
            <div>
                <div>
                    {/*this.state.sensorData.map(sensor => <p>{sensor.dataPoint.id}: {dataPoint.personCount} people at {dataPoint.timeStamp}</p>)*/}
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
                        {this.state.sensorData.map(s =>
                            <LineSeries data={s.map(dataPoint => ({ x: new Date(dataPoint.timeStamp), y: dataPoint.personCount }))} />)
                        }
                    </XYPlot>}
            </div>
        );
    }
}