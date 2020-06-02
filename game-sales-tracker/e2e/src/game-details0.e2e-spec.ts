import { AppPage } from "./app.po";
import { browser, logging, by, element } from "protractor";

describe("game details page", () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
    browser.get("/game/0");
    browser.waitForAngularEnabled(false);
  });

  it("should display game info", () => {
    expect(page.getElement(".main").isPresent()).toBeTruthy();
  });

  it("should navigate to details", () => {
    browser.getCurrentUrl().then((url) => {
      expect(url).toContain("game/0");
    });
  });

  it("should display game name", () => {
    browser.driver.sleep(1000);
    expect(page.getElement(".price-block").isPresent()).toBeTruthy();
    expect(page.getTextByElement(".price-block .name")).toBeTruthy();
  });

  it("should display game details", () => {
    browser.driver.sleep(1000);
    expect(page.getElement(".details").isPresent()).toBeTruthy();
    expect(page.getTextByElement(".description")).toBeTruthy();
    expect(page.getTextByElement(".details .name")).toBeTruthy();
  });

  afterEach(async () => {
    // Assert that there are no errors emitted from the browser
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(
      jasmine.objectContaining({
        level: logging.Level.SEVERE,
      } as logging.Entry)
    );
  });
});
