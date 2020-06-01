import { AppPage } from "./app.po";
import { browser, logging, by, element } from "protractor";

describe("main page", () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
    page.navigateTo("/");
    browser.waitForAngularEnabled(false);
  });

  it("should navigate to details", async () => {
    browser.getCurrentUrl().then((url) => {
      expect(url).toEqual("http://localhost:4200/");
    });
  });

  it("should display countdown", () => {
    expect(page.getElement("app-countdown").isPresent()).toBeTruthy();
  });

  it("should display 'see more games' in tag a", () => {
    expect(page.getElement(".more-games").getText()).toEqual("See more games");
  });

  it("should have 4 game in carousel", () => {
    expect(page.countElements("app-carousel .game")).toEqual(4);
  });

  it("should display list of platforms", () => {
    expect(page.countElements(".options mat-checkbox")).toBeGreaterThan(0);
  });

  it("should display list of games", () => {
    expect(page.countElements(".game-container .game")).toBe(10);
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
