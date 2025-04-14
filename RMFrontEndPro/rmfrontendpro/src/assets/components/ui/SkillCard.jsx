import PropTypes from 'prop-types';
import './../../styles/components/skills.css';

const SkillCard = ({ name, level, image }) => {
    return (
        <div className="skill-card">
            <div className="skill-image-container">
                <img
                    src={image}
                    alt={name}
                    className="skill-image"
                    loading="lazy"
                />
            </div>
            <h3 className="skill-name">{name}</h3>
            <div className="skill-level">
                <div className="skill-progress">
                    <div
                        className="skill-progress-bar"
                        style={{ width: `${level}%` }}
                    ></div>
                </div>
                <span className="skill-percentage">{level}%</span>
            </div>
        </div>
    );
};

SkillCard.propTypes = {
    name: PropTypes.string.isRequired,
    level: PropTypes.number.isRequired,
    image: PropTypes.string.isRequired
};

export default SkillCard;