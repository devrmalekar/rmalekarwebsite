
const ErrorScreen = ({ error }) => {
   return ( <div className="error-screen">
        <Error className="alert alert-danger" role="alert">
            <h2> Opps! Something went wrong on my end, working on it and will have everything backup shortly. Please check back in a bit!</h2>
            <p>{error}</p>
            <button onClick={() => window.location.reload()}>Retry</button> 
        </Error>
    </div>
   )};

export default ErrorScreen;