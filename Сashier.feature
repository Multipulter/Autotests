﻿Feature: проверка функционала кассира
    
Scenario: авторизация кассира
    Given Я открываю страницу авторизации
    And Я ввожу логин и пароль учетной записи кассира
    And Нажимаю кнопку 'ВХОД'
    And Я выбираю кассу
    And Я ввожу данные в консоль браузера
    