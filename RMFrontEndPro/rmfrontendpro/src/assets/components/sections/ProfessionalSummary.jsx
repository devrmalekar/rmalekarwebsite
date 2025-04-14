import '../../styles/components/personalsummary.css';

const About = ({ personalData }) => {
    return (
        <section id="about" className="about-section container-fluid px-0">
            <div className="container">
                <div className="row align-items-center">
                    <div className="col-lg-6">
                        <h2 className="section-title">Professional Summary</h2>
                        <div className="about-content">
                            <p>{personalData.summary}</p>
                            <div className="personal-info">
                                <div className="info-item">
                                    <span>Name:</span>
                                    <span>{personalData.FirstName} {personalData.LastName}</span>
                                </div>
                                <div className="info-item">
                                    <span>Email:</span>
                                    <span>{personalData.Contact["email"]}</span>
                                </div>
                                <div className="info-item">
                                    <span>Experience:</span>
                                    <span>{personalData.ExperienceYears}+ years</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="col-lg-6">
                        <div className="about-image">
                            <img
                                src="\images\about.jpg"
                                alt="About Me"
                                className="img-fluid rounded"
                            />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default About;
