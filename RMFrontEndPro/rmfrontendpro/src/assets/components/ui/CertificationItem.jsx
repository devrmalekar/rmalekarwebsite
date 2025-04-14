import PropTypes from 'prop-types';
import '../../styles/components/certification.css';

const CertificationItem = ({ title, issuer, date, link }) => {

    return (
        <div className="certificate-item">
            
            <div className="certificate-content">
                <h4>{title}</h4>
               
                <div className="row">
                    <div className="col-sm-6">
                        <p>{issuer}</p>
                    </div>
                    <div className="col-sm-6">
                        <span className="timeline-duration">{date}</span>
                    </div>
                </div>
                <div className="certificate-links">
                    {link && (
                        <a href={link} target="_blank" rel="noopener noreferrer">
                            <i className="fas fa-link"></i> Show Credentials
                        </a>
                    )}
                </div>
            </div>
        </div>
    );
};


export default CertificationItem;