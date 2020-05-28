const settings = require('./appsettings.json');
const request = require('request');
const amqp = require('amqplib/callback_api');

const parsedData = [];
let gameCount = 0;

const postData = {
    "query": "query collectionLayoutQuery($locale: String, $country: String!, $slug: String) {\n  Storefront {\n    collectionLayout(locale: $locale, slug: $slug) {\n      _activeDate\n      _locale\n      _metaTags\n      _slug\n      _title\n      _urlPattern\n      lastModified\n      regionBlock\n      affiliateId\n      takeover {\n        banner {\n          altText\n          src\n        }\n        description\n        eyebrow\n        title\n      }\n      seo {\n        title\n        description\n        keywords\n        image {\n          src\n          altText\n        }\n        twitter {\n          title\n          description\n        }\n        og {\n          title\n          description\n          image {\n            src\n            alt\n          }\n        }\n      }\n      collectionOffers {\n        title\n        id\n        namespace\n        description\n        effectiveDate\n        keyImages {\n          type\n          url\n        }\n        seller {\n          id\n          name\n        }\n        productSlug\n        urlSlug\n        url\n        items {\n          id\n          namespace\n        }\n        customAttributes {\n          key\n          value\n        }\n        categories {\n          path\n        }\n        linkedOfferId\n        linkedOffer {\n          effectiveDate\n          customAttributes {\n            key\n            value\n          }\n        }\n        price(country: $country) {\n          totalPrice {\n            discountPrice\n            originalPrice\n            voucherDiscount\n            discount\n            fmtPrice(locale: $locale) {\n              originalPrice\n              discountPrice\n              intermediatePrice\n            }\n          }\n          lineOffers {\n            appliedRules {\n              id\n              endDate\n            }\n          }\n        }\n      }\n    }\n  }\n}\n",
    "variables": {
        "locale": "ru",
        "slug": "mega-sale-games",
        "country": "UA"
    }
}

function httpPost() {
    request.post(
        settings.url,
        {
            json: postData,
            headers: {
                "Content-Type": "application/json"
            }
        },
        (error, response, body) => {
            if (error)
                console.log(error)
            if (!error && response.statusCode === 200) {
                parse(body.data.Storefront.collectionLayout.collectionOffers);
            }
        }
    );
}

function parse(data) {
    gameCount = data.length;
    data.forEach((game, i) => {
        const entry = {};
        let { title, productSlug, keyImages, price } = game;
        entry.Name = title;
        if (!productSlug) {
            --gameCount;
            return;
        }
        if (productSlug.slice(-5) == '/home')
            productSlug = productSlug.slice(0, -5);

        entry.PlatformSpecificId = productSlug;
        entry.PlatformId = settings.platformId;
        entry.CurrencyId = settings.currencyId;
        entry.BasePrice = price.totalPrice.originalPrice;
        entry.DiscountedPrice = price.totalPrice.discountPrice;
        if (!keyImages.find(x => x.type === "DieselStoreFrontWide"))
            entry.PictureURLs = [keyImages[0].url]
        else
            entry.PictureURLs = [keyImages.find(x => x.type === "DieselStoreFrontWide").url];

        httpGetDescription(entry);
    })
}

function httpGetDescription(game) {
    request.get(
        settings.descrUrl + game.PlatformSpecificId,
        {
            headers: {
                "Content-Type": "application/json"
            }
        },
        (error, response, body) => {
            if (error)
                console.log(error)
            if (response.statusCode !== 200) --gameCount;
            if (!error && response.statusCode === 200) {
                const images = JSON.parse(body).pages[0].data.gallery.galleryImages;
                if (images) game.PictureURLs.push(...images.map(x => x.src));
                game.PictureURLs = game.PictureURLs.map(x => x = x.replace(/\s/g, '%20'));
                game.Description = JSON.parse(body).pages[0].data.about.description;
                parsedData.push(game);
                if (gameCount === parsedData.length)
                    sendMessage(JSON.stringify(parsedData));
            }
        }
    );
}

function sendMessage(message) {
    amqp.connect(settings.queueSettings.connectionString, function (error0, connection) {
        if (error0) {
            throw error0;
        }
        connection.createChannel(function (error1, channel) {
            if (error1) {
                throw error1;
            }

            const queue = settings.queueSettings.queueName;

            channel.assertQueue(queue, {
                durable: false
            });
            channel.sendToQueue(queue, Buffer.from(msg));
        });
        setTimeout(function () {
            connection.close();
            process.exit(0);
        }, 500);
    });
}

httpPost();
