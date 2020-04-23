const jwt = require('jsonwebtoken');
const secret = require('../../config/auth').secret;

module.exports = (req, res, next) => {

    const [token_type, jwt_token] = req.headers['authorization'].split(' ');

    jwt.verify(jwt_token, secret, (err, result) => {
        if (err)
            return res.status(401).json({ error: err });
        req.user = result;
        return next();
    });
};
