import { useRef, useState } from 'react';
import '../../styles/components/contact.css';

const Contact = ({ personalData, getSocialLinks }) => {
    const formRef = useRef();
    const [contactRespondMsg, setResponseMsg] = useState("");
    const [hasContacted, setHasContacted] = useState(false);
    const handleSubmit = (e) => {
        e.preventDefault();

        var senderName = e.target.senderName.value;
        //console.log(e.target.senderName.value);
        const formData = {
            Name: senderName,
            Email: e.target.senderEmail.value,
            Subject: e.target.subject.value,
            Message: e.target.message.value
        };

        fetch('https://webapi.rmalekar.com.np/email/sendemail', {
            method: 'POST',
            headers: { 'content-type': 'application/json' },
            body: JSON.stringify(formData)
        })
            .then(response => {
                setHasContacted(true);

                if (response.ok) {
                    setResponseMsg("Thank you for contacting! Will get back to you asap.");
                }
                else {
                    setResponseMsg("Oops! Something went wrong. Please email your message to devrmalekar@com.np");
                }
            })
            .catch(error => {
                console.log(error);
                setHasContacted(true);
                setResponseMsg("Oops! Something went wrong. Please email your message to devrmalekar@com.np");

            });

                 

       
       // formRef.current.reset();
    };

    return (
        <section id="contact" className="contact-section container-fluid px-0">
            <div className="container">
                <h2 className="section-title">Get In Touch</h2>
                <div className="row">
                    <div className="col-lg-5">
                        <div className="contact-info">
                            <h3>Contact Information</h3>
                            <ul>
                                <li>
                                    <i className="fas fa-map-marker-alt"></i>
                                    <span>{personalData.Contact["address"]}</span>
                                </li>
                                <li>
                                    <i className="fas fa-phone"></i>
                                    <span>{personalData.Contact["mobile"]}</span>
                                </li>
                                <li>
                                    <i className="fas fa-envelope"></i>
                                    <span>{personalData.Contact["email"]}</span>
                                </li>
                            </ul>
                            <div className="social-links">
                                {getSocialLinks(personalData)}
                            </div>
                        </div>
                    </div>
                    <div className={`col-lg-7 ${hasContacted ? "response-msg": ""}`}>
                        {hasContacted ? (
                            <p>{contactRespondMsg}</p>

                        ): (
                                <form ref = { formRef } onSubmit = { handleSubmit } className = "contact-form">
                            <div className = "form-group">
                                <input
                                    type = "text"
                                    id = "senderName"
                                    className = "form-control"
                                    placeholder = "Your Name"
                                    required
                                />
                    </div>
                    <div className="form-group">
                        <input
                            type="email"
                            id="senderEmail"
                            className="form-control"
                            placeholder="Your Email"
                            required
                        />
                    </div>
                    <div className="form-group">
                        <input
                            type="text"
                            id="subject"
                            className="form-control"
                            placeholder="Subject"
                            required
                        />
                    </div>
                    <div className="form-group">
                        <textarea
                            className="form-control"
                            id="message"
                            rows="5"
                            placeholder="Your Message"
                            required
                        ></textarea>
                    </div>
                    <button type="submit" className="submit-btn">
                        Send Message
                    </button>
                </form>

                            ) }

            </div>
        </div>
            </div >
        </section >
    );
};

export default Contact;
