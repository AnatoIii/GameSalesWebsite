package com.gamesales.nintendoparser.service.impl;

import com.gamesales.nintendoparser.model.Game;
import com.gamesales.nintendoparser.service.GameParserService;
import com.gamesales.nintendoparser.service.GameRequestService;
import com.gamesales.nintendoparser.service.GameService;
import lombok.extern.slf4j.Slf4j;
import org.jsoup.nodes.Document;
import org.springframework.stereotype.Service;

import java.util.Collection;

@Slf4j
@Service
public class GameServiceImpl implements GameService {
    private final GameRequestService requestService;
    private final GameParserService parserService;

    public GameServiceImpl(GameRequestService requestService, GameParserService parserService) {
        this.requestService = requestService;
        this.parserService = parserService;
    }

    @Override
    public Collection<Game> getGameInfosByName(String name) {
        Document gameDoc = requestService.loadDocumentByGameName(name);
        return parserService.parseDocument(gameDoc);
    }
}
