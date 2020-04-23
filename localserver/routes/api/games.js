const express = require('express')
const fs = require('fs')
const router = express.Router()
let games = JSON.parse(fs.readFileSync('data/games.json', 'utf8'))

router.get('/games', (req, res) => {
    res.json(games)
})

router.get('/games/:id', (req, res) => {
    res.json(games.find(game => game.id == req.params.id))
})

router.post('/games', (req, res) => {
    const { name, description, price, image, date } = req.body
    const newId = Math.max(...games.map(game => game.id), 0) + 1
    games.push({ id: newId, name, description, price, image, date })
    saveGames(games)
    res.json(games)
})

router.delete('/games/:id', (req, res) => {
    const index = games.indexOf(games.find(game => game.id == req.params.id))
    if (index !== -1) games.splice(index, 1)
    saveGames(games)
})

router.put('/games/:id', (req, res) => {
    const { name, description, price, image, date } = req.body
    let gameToChange = games.find(game => game.id == req.params.id)
    gameToChange.name = name
    gameToChange.description = description
    gameToChange.price = price
    gameToChange.image = image
    gameToChange.date = date
    saveGames(games)
})

function saveGames(notes) {
    fs.writeFileSync('games.json', JSON.stringify(games))
}

module.exports = router;