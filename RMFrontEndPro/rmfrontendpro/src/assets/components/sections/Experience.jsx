import TimelineItem from '../ui/TimelineItem';
import '../../styles/components/experience.css';

const Experience = ({ experiences }) => {
    return (
        <section id="experience" className="experience-section container-fluid px-0">
            <div className="container">
                <h2 className="section-title">Work Experience</h2>
                <div className="timeline">
                    {experiences.map((exp, index) => (
                        <TimelineItem
                            key={index}
                            position={exp.Position}
                            company={exp.CompanyName}
                            address={exp.CompanyAddr }
                            duration={`${exp.StartDate} - ${(exp.EndDate) ? (exp.EndDate) : "Present"}`}
                            skills={exp.KeySkills.split(";").filter(Boolean)}
                            isLast={index === experiences.length - 1}
                        />
                    ))}
                </div>
            </div>
        </section>
    );
};

export default Experience;