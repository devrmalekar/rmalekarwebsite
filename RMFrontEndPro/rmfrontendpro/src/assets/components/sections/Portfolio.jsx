import PortfolioItem from '../ui/PortfolioItem';
import '../../styles/components/portfolio.css';

const Portfolio = ({ portfolioItems }) => {
    return (
        <section id="portfolio" className="portfolio-section container-fluid px-0">
            <div className="container">
                <h2 className="section-title">My Portfolio</h2>
                {/* Portfolio Grid */}
                <div className="portfolio-grid">
                    {portfolioItems.map((item, index) => (
                        <PortfolioItem
                            key={index}
                            title={item.Title}
                            image={item.Thumbnail}
                            description={item.Description}
                            skills={item.Skills}
                            link={item.CodeLink}
                        />
                    ))}
                </div>
            </div>
        </section>
    );
};

export default Portfolio;