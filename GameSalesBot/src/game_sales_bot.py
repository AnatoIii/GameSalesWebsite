from bot import TelegramBot


class GameSalesBot(TelegramBot):
    def __init__(self, token):
        super(GameSalesBot, self).__init__(token)
