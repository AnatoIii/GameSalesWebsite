package com.gamesales.nintendoparser.service;

import org.jsoup.nodes.Document;

public interface GameRequestService {
    Document loadDocumentByGameName(String gameName);
}
