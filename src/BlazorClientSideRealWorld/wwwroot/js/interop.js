window.RealWorld = {
    saveToken: function (token) {
        window.localStorage.setItem('jwt', token);
        console.log("Authentication token has been stored.");
        return true;
    },
    getToken: function () {
        var token = window.localStorage.getItem('jwt');
        console.log(token ? "Authentication token read from storage." : "No authentication token found in storage.");
        return token;
    },
    destroyToken: function () {
        window.localStorage.removeItem('jwt');
        console.log("Authentication token has been deleted.");
        return true;
    },
    consoleLog: function (message) {
        console.log(message);
        return true;
    },
    scrollToTop: function () {
        window.scrollTo(0, 0);
        return true;
    }
};