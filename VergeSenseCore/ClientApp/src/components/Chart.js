import React, { Component } from 'react';
import { XYPlot, XAxis, YAxis, HorizontalGridLines, VerticalGridLines, LineSeries } from 'react-vis';
import Datetime from 'react-datetime';

const API = 'api/Sensor/SensorData';
const colors = ["#12939a", "#79c7e3", "#1a3177"];

export class ChartComponent extends Component {

    constructor(props) {
        super(props);

        this.state = {
            sensors: [],
            loading: false,
            start: null,
            end: null,
            hasQ: false
        };

        this.loadData();
    }

    buildUrl() {
        let qs = this.state.start === null || this.state.start === "" ? "" : "start=" + this.state.start.toISOString();
        let qe = this.state.end === null || this.state.end === "" ? "" : "end=" + this.state.end.toISOString();
        if (qs !== "" && qe !== "") return API + "?" + qs + "&" + qe;
        if (qs !== "") return API + "?" + qs;
        if (qe !== "") return API + "?" + qe;
        return API;
    }

    loadData() {
        let url = this.buildUrl(); 
        fetch(url)
            .then(response => response.json())
            .then(data => {
                this.setState({ sensors: data, loading: false });
            });
    }

    clear() {
        this.setState({start: null, end:null }, this.loadData);
    }

    handleDateChange() {
        this.setState({ loading: true }, this.loadData);
    }

    handleStartChange(startDate) {
        this.setState({ start: startDate }, this.handleDateChange);
    }

    handleEndChange(endDate) {
        this.setState({ end: endDate }, this.handleDateChange);
    }

    renderCombinedPlot() {
        return (
            this.state.sensors.map((sensor, i) =>
                <LineSeries key={i} data={sensor.data.map(dataPoint => ({ x: new Date(dataPoint.timeStamp), y: dataPoint.personCount }))} />)
        );
    }

    renderIndividualPlot(sensor, i) {
        return (
            <div key={sensor.id}>
                <h3>{sensor.id}</h3>
                <XYPlot
                    xType="time"
                    height={300}
                    width={900}
                    padding={1}
                >
                    <VerticalGridLines />
                    <HorizontalGridLines />
                    <XAxis />
                    <YAxis />
                    <LineSeries />
                    <LineSeries
                        color={colors[i]}
                        data={sensor.data.map(dataPoint => ({ x: new Date(dataPoint.timeStamp), y: dataPoint.personCount }))}
                    />
                </XYPlot>
            </div>
            );
    }

    renderIndividualPlots() {
        return (
            <div>
                {this.state.sensors.map((sensor, i) => sensor.data.length > 0 ? this.renderIndividualPlot(sensor, i) : "")}
            </div>
        );
    }

    render() {
        let sensorPlot = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCombinedPlot();
        let individualPlots = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderIndividualPlots();

        return (
            <div>
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
                <button onClick={() => this.clear()}>Clear</button>
                {this.state.loading
                    ? <p><em>Loading...</em></p>
                    : <div>
                        <h3>Combined Chart ({this.state.sensors.length} sensors)</h3>
                        <XYPlot
                            xType="time"
                            height={300}
                            width={900}
                        >
                        <VerticalGridLines />
                        <HorizontalGridLines />
                        <XAxis />
                        <YAxis />
                        {sensorPlot}
                        </XYPlot>
                        <div className={"individual"}>
                            {individualPlots}
                        </div>
                    </div>
                }
            </div>
        );
    }
}