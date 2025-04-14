import PropTypes from 'prop-types';
import '../../styles/components/portfolio.css';

const PortfolioItem = ({ title, image, description, skills, link }) => {

    return (
        <div className="portfolio-item">
            <div className="portfolio-image">
                <img src={`/images/portfolio/${image}`} alt={title} loading="lazy" />
                
            </div>
            <div className="portfolio-content">
                <h4>{title}</h4>
                <p>{description}</p>
                <div className="portfolio-tags">
                 
                    {skills && skills.split(",").filter(Boolean).map((tag, index) => (
                        <span key={index} className="tag">{tag}</span>
                    ))}
                </div>
                <div className="portfolio-links">
                    {link && (
                        <a href={link} target="_blank" rel="noopener noreferrer">
                            <i className="fab fa-github"></i> Code
                        </a>
                    )}
                </div>
            </div>
        </div>
    );
};

PortfolioItem.propTypes = {
    title: PropTypes.string.isRequired,
    image: PropTypes.string.isRequired,
    category: PropTypes.string.isRequired,
    description: PropTypes.string.isRequired,
    tags: PropTypes.arrayOf(PropTypes.string).isRequired,
    links: PropTypes.shape({
        demo: PropTypes.string,
        code: PropTypes.string
    }).isRequired
};

export default PortfolioItem;