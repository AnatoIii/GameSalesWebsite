const express = require('express');
const cors = require('cors');
const app = express();

const log = require('./routes/middleware/log');
const auth = require('./routes/middleware/auth');

const loginRouter = require('./routes/api/login');
const userRouter = require('./routes/api/profile');
const gamesRouter = require('./routes/api/games');

app.use(express.json());
app.use(cors());
app.use(log);

app.use('/api', loginRouter);
app.use('/api', gamesRouter);

app.use(auth);
app.use('/api', userRouter);


app.listen(8082);
