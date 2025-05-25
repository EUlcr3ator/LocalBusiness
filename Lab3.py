from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from webdriver_manager.chrome import ChromeDriverManager
from selenium.webdriver.chrome.service import Service as ChromeService


from webdriver_manager.chrome import ChromeDriverManager
from selenium.webdriver.chrome.service import Service as ChromeService

def test_successful_login():

    driver = webdriver.Chrome(service=ChromeService(ChromeDriverManager().install()))
    driver.maximize_window()


    driver.get("https://opensource-demo.orangehrmlive.com/")

    try:
        wait = WebDriverWait(driver, 10)
        username_input = wait.until(EC.presence_of_element_located((By.NAME, "username")))
        password_input = driver.find_element(By.NAME, "password")

        username_input.send_keys("Admin")
        password_input.send_keys("admin123")

        login_button = driver.find_element(By.XPATH, "//button[@type='submit']")
        login_button.click()

        dashboard = wait.until(EC.presence_of_element_located((By.TAG_NAME, "h6")))
        assert dashboard.text == "Dashboard", f"Очікував 'Dashboard', отримав: {dashboard.text}"

        print("Успішний вхід — тест пройдено!")
    except Exception as e:
        print("Тест не пройдено:", e)
    finally:
        driver.quit()


def test_invalid_login():
    driver = webdriver.Chrome(service=ChromeService(ChromeDriverManager().install()))
    driver.maximize_window()
    driver.get("https://opensource-demo.orangehrmlive.com/")

    try:
        wait = WebDriverWait(driver, 10)

        username_input = wait.until(EC.presence_of_element_located((By.NAME, "username")))
        password_input = driver.find_element(By.NAME, "password")

        username_input.send_keys("Admin")
        password_input.send_keys("wrongpassword")

        login_button = driver.find_element(By.XPATH, "//button[@type='submit']")
        login_button.click()

        error_message = wait.until(EC.presence_of_element_located((
            By.CSS_SELECTOR, "p.oxd-text.oxd-text--p.oxd-alert-content-text"
        )))

        assert "Invalid credentials" in error_message.text, f"Очікував повідомлення про помилку, отримав '{error_message.text}'"

        print("Невірний вхід — повідомлення про помилку з'явилось! Тест пройдено!")
    except Exception as e:
        print("Тест не пройдено:", e)
    finally:
        driver.quit()

def test_navigate_to_my_info():
    driver = webdriver.Chrome(service=ChromeService(ChromeDriverManager().install()))
    driver.maximize_window()
    driver.get("https://opensource-demo.orangehrmlive.com/")

    try:
        wait = WebDriverWait(driver, 10)

        username_input = wait.until(EC.presence_of_element_located((By.NAME, "username")))
        password_input = driver.find_element(By.NAME, "password")
        username_input.send_keys("Admin")
        password_input.send_keys("admin123")
        login_button = driver.find_element(By.XPATH, "//button[@type='submit']")
        login_button.click()

        my_info_link = wait.until(EC.presence_of_element_located((
            By.XPATH, "//span[text()='My Info']"
        )))
        my_info_link.click()

        page_header = wait.until(EC.presence_of_element_located((
            By.XPATH, "//h6[text()='Personal Details']"
        )))

        assert page_header.text == "Personal Details", f"Очікував 'Personal Details', отримав: {page_header.text}"

        print("Перехід на сторінку 'My Info' — тест пройдено!")
    except Exception as e:
        print("Тест не пройдено:", e)
    finally:
        driver.quit()


if __name__ == "__main__":
    test_successful_login()
    #test_invalid_login()
    #test_navigate_to_my_info()
