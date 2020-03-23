const jwt = require('jsonwebtoken');
const express = require('express');
const fs = require('fs')
const router = express.Router()
let users = JSON.parse(fs.readFileSync('data/users.json', 'utf8'))

const secret = require('../../config/auth').secret;

router.post('/login', (req, res) => {
    let {email, password} = req.body;

    let [user] = users.filter(user => (user.email == email && user.password == password));
  
    if(!user) {
        res.status(401).json({status: 'User not found'});
    }

    let jwt_token = jwt.sign(user, secret);
    res.json({jwt_token: jwt_token});
});

module.exports = router;