import { browser, by, element } from "protractor";

export class AppPage {
  navigateTo(url): Promise<unknown> {
    return browser.get(url) as Promise<unknown>;
  }

  getTextByElement(selector) {
    return element(by.css(selector)).getText();
  }

  clickByElement(selector) {
    element(by.css(selector)).click();
  }

  getElement(selector) {
    return element(by.css(selector));
  }
}
