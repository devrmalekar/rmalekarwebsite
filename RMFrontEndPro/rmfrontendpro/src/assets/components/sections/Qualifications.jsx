import '../../styles/components/qualifications.css';

const Qualifications = ({ qualifications }) => {
    return (
        <section id="qualifications" className="qualifications-section container-fluid px-0">
            <div className="container">
                <h2 className="section-title">My Qualifications</h2>
                <div className="timeline">
                {
                    Object.entries(qualifications).map(([degreeType, item], index) =>
                    (
                        <div key={degreeType} className={`timeline-item ${(index - 1 === 1) ? 'last' : ''}`}>
                            <div className="timeline-content">
                                <div className="timeline-header">
                                    <h5>{item.Degree}</h5>
                                    <div className="timeline-institute row">
                                        <div className="col-12 col-sm-8">
                                            <span>{item.Institute} - {item.Addr}</span>
                                        </div>

                                        <div className="col-12 col-sm-4 td-container">
                                            <span className="timeline-duration">{item.StartDate} - {item.EndDate}</span>
                                        </div>
                                    </div>
                                    <br />
                                    <div className="timeline-body">
                                        <div className="timeline-description">
                                            <span><p><b>GPA: {item.GPA}</b></p></span>
                                            <span><p><b>Key Subjects:</b></p></span>
                                            {item.KeySubjects.split(';').filter(Boolean).map((subject, index) => (
                                                <span key={index}>
                                                    <span>{subject}</span>
                                                    {index !== subject.length - 1 && (
                                                        <span style={{ color: "red", margin: "0 5px" }}>|</span>
                                                    )}
                                                </span>
                                            ))}

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    )
                    )
                    }
                </div>
            </div>
        </section>
    );
};

export default Qualifications;