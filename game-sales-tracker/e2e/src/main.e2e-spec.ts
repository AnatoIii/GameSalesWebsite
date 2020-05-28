import { AppPage } from "./app.po";
import { browser, logging, by, element } from "protractor";

describe("main page", () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
    page.navigateTo("/");
    browser.waitForAngularEnabled(false);
  });

  it("should display countdown", () => {
    expect(page.getElement("app-countdown").isPresent()).toBeTruthy();
  });

  it("should display 'see more games' in tag a", () => {
    expect(page.getElement(".more-games").getText()).toEqual("See more games");
  });

  afterEach(async () => {
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(
      jasmine.objectContaining({
        level: logging.Level.SEVERE,
      } as logging.Entry)
    );
  });
});
