import React from 'react';
import ReactDOM from 'react-dom';
import { ChartComponent } from './components/Chart';
import './index.css';

class VergeSenseApp extends React.Component {
    render() {
        return (
            <ChartComponent />
        );
    }
}

ReactDOM.render(<VergeSenseApp />, document.getElementById('root'));