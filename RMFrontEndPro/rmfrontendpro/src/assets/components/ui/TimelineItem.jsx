import PropTypes from 'prop-types';
import '../../styles/components/timeline.css';


const TimelineItem = ({ position, company, address, duration, skills, isLast }) => {
    return (
        <div className={`timeline-item ${isLast ? 'last' : ''}`}>
            <div className="timeline-content">
                <div className="timeline-header">
                    <h3>{position}</h3>
                    <div className="timeline-company">
                        <div className="row">
                            <div className="col-12 col-sm-8">
                                <span>{company}</span><br/>
                                <span>{address}</span>
                            </div>
                            <div className="col-12 col-sm-4 td-container">
                                <span className="timeline-duration">{duration}</span>
                            </div>
                        </div>
                    </div>
                </div> <br/>
                <div className="timeline-body">
                    <h5>Key Skills:</h5>
                    {skills.map((skill, index) => (
                        <span key={index}>
                            <span>{skill}</span>
                            {index !== skill.length - 1 && (
                                <span style={{ color: "red", margin: "0 5px" }}>|</span>
                            )}
                        </span>
                    ))}
                    {/* <ul>
                        {responsibilities.map((item, index) => (
                            <li key={index}>{item}</li>
                        ))}
                    </ul> */}
                </div>
            </div>
        </div>
    );
};

TimelineItem.propTypes = {
    position: PropTypes.string.isRequired,
    company: PropTypes.string.isRequired,
    duration: PropTypes.string.isRequired,
    responsibilities: PropTypes.array.isRequired,
    isLast: PropTypes.bool
};

export default TimelineItem;