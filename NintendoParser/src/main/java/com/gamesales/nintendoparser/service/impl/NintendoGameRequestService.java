package com.gamesales.nintendoparser.service.impl;

import com.gamesales.nintendoparser.service.GameRequestService;
import lombok.extern.slf4j.Slf4j;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import java.io.IOException;

@Slf4j
@Service
public class NintendoGameRequestService implements GameRequestService {

    @Value("${nintendo.store.url}")
    private String nintendoStoreUrl;

    @Value("${nintendo.catalog.uri}")
    private String nintendoCatalogUri;

    @Override
    public Document loadDocumentByGameName(String gameName) {
        String fullUrl = nintendoStoreUrl + nintendoCatalogUri + gameName;
        try {
            return Jsoup.connect(nintendoStoreUrl + nintendoCatalogUri + gameName).get();
        } catch (IOException e) {
            log.debug(e.getMessage());
            return null;
        }
    }
}
