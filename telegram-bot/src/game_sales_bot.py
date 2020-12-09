from telegram import Update
from telegram.ext import CommandHandler, CallbackContext

from bot import TelegramBot, ParseMode
from exception_handler import exceptionHandle
from services.game_service import GameService


class GameSalesBot(TelegramBot):
    def __init__(self, token):
        super(GameSalesBot, self).__init__(token)
        self._game_service = GameService()
        self._dispatcher.add_handler(CommandHandler('sales', self._get_best_games))

    @exceptionHandle
    def _get_best_games(self, update: Update, context: CallbackContext):
        chat_id = update.effective_user.id
        games = self._game_service.get_best_games(10)
        context.bot.send_message(chat_id, "<b>Top 10 game sales:</b>", parse_mode=ParseMode.HTML)
        for game in games:
            game_text = "%s\n" % game.name
            game_text += "Price: %.2fUAH\n" % (game.price / 100)
            game_text += "%s\n" % self._append_platforms_links(game.platforms)
            game_text += "%s\n" % (game.desc[:300] + '..') if len(game.desc) > 300 else game.desc
            if game.image:
                context.bot.send_photo(chat_id=update.effective_user.id,
                                       photo=game.image,
                                       caption=game_text,
                                       parse_mode=ParseMode.HTML)
            else:
                context.bot.send_message(chat_id=update.effective_user.id,
                                         text=game_text,
                                         parse_mode=ParseMode.HTML)

    def _append_platforms_links(self, platforms_ids):
        platform_text = ""
        for platform in platforms_ids:
            if platform == 1:
                platform_text += "<a href='%s'>PS Store</a> " % self._cfg['game_store']['ps']
            elif platform == 2:
                platform_text += "<a href='%s'>Steam</a> " % self._cfg['game_store']['steam']
            elif platform == 3:
                platform_text += "<a href='%s'>Uplay</a> " % self._cfg['game_store']['uplay']
            elif platform == 4:
                platform_text += "<a href='%s'>Nintendo</a> " % self._cfg['game_store']['nintendo']
            elif platform == 5:
                platform_text += "<a href='%s'>Epic Store</a> " % self._cfg['game_store']['epic']

        return platform_text
