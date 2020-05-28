import { AppPage } from "./app.po";
import { browser, logging, by, element } from "protractor";

describe("game filter page", () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
    page.navigateTo("/games/0");
    browser.waitForAngularEnabled(false);
  });

  it("should display options", () => {
    expect(page.getElement(".options").isPresent()).toBeTruthy();
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
