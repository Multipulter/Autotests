﻿Feature: Клиентский сайт - Проверка выбора вакансии 

Scenario: Проверка выбора вакансии "Кассир"
    Given Я захожу на клиентский сайт
    And Я перехожу в раздел 'Вакансии'
    And Я нажимаю на кнопку 'Заполнить анкету'
    And Я нажимаю на кнопку 'Выбрать' в окне 'Кассир'
    And Я вижу название вакансии 'Кассир'