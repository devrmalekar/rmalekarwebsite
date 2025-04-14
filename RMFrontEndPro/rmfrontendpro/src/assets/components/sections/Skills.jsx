import { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Swiper, SwiperSlide } from 'swiper/react';
import { Navigation, Pagination, Autoplay } from 'swiper/modules';
import SkillCard from '../ui/SkillCard';
import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import '../../styles/components/skills.css';

const Skills = ({ skills }) => {
    const handleFilteredItems = () => {
        let allSkills = {};
        Object.keys(skills).forEach(category => {
            Object.keys(skills[category]).forEach(subCategory => {
                if (!allSkills[subCategory]) {
                    allSkills[subCategory] = [];
                }
                allSkills[subCategory] = [...allSkills[subCategory], ...skills[category][subCategory]];
            });
        })
        return allSkills;
    }

    const [activeFilter, setActiveFilter] = useState('Core Technical Skills');
    const filteredItems = activeFilter === 'all'
        ? handleFilteredItems()
        : skills[activeFilter];

   

    return (
        <section id="skills" className="skills-section container-fluid px-0 parallax">
            <div className="container">
                <h2 className="section-title">Stack</h2>
                {/* Filter Buttons */}
                < div className="portfolio-filters" >
                    <button
                        className={`category-btn ${activeFilter === 'all' ? 'active' : ''}`}
                        onClick={() => setActiveFilter('all')}
                    >
                        All
                    </button>
                    {Object.entries(skills).map(([mainCategory, subCategories], index) => (
                        <button
                            key={index + subCategories }
                            className={`filter-btn ${activeFilter === mainCategory ? 'active' : ''}`}
                            onClick={() => setActiveFilter(mainCategory)}
                        >
                            {mainCategory}
                        </button>
                    ))}
                </div>

                {/* Portfolio Grid */}
                <div className="skills-categories">
                    {Object.entries(filteredItems).map(([subcategory, catskills]) => (
                        <div key={subcategory} className="sub-category">
                            <div className="sub-category-content">
                                <h5 >{subcategory}</h5>

                                <div className="skills-container">
                                    {catskills.map((skill, i) => (
                                        <div key={i} className="skill-item">
                                            <i className={`skill-icon ${skill.SkillLogo}`}></i>

                                            <span>{skill.Skill}</span>
                                        </div>
                                    ))}
                                </div>
                            </div>
                            
                        </div>
                    ))}
                </div>
            </div>
        </section>
    );
};


export default Skills;