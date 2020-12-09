package com.gamesales.nintendoparser.controller;

import com.gamesales.nintendoparser.model.Game;
import com.gamesales.nintendoparser.service.GameService;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import java.util.Collection;

@RestController
@RequestMapping("/api/games")
public class NintendoApiController {

    private final GameService gameService;

    public NintendoApiController(GameService gameService) {
        this.gameService = gameService;
    }

    @RequestMapping(path = "/{gameName}", method = RequestMethod.GET)
    public Collection<Game> getGameInfosByName(@PathVariable String gameName) {
        return gameService.getGameInfosByName(gameName);
    }
}
