﻿Feature: проверка функционала центрального администратора.
    Центральльный администратор выполняет множество разных функций. в том чилсе управление подразделениями и торговой зоной.
    
Scenario: Добавление контрагента
    Given Я открываю страницу авторизации
    And Я ввожу логин и пароль учетной записи центрального администратора
    And Нажимаю кнопку 'ВХОД'
    And Я выбираю роль 'Центральный администратор'
    And Я нажимаю на пункт меню 'Контрагенты'
    And Я нажимаю на кнопку 'Добавить контрагента'
    And Я ввожу название контрагента: 'Контрагент'
    And Я ввожу данные в поле 'Полное наименование'
    And Я нажимаю на кнопку 'Сохранить'
    And Я вижу созданного контрагента: 'Контрагент'
    And Я удаляю созданного контрагента: 'Контрагент'

    Scenario: Добавление подразделения
        Given Я открываю страницу авторизации
        And Я ввожу логин и пароль учетной записи центрального администратора
        And Нажимаю кнопку 'ВХОД'
        And Я выбираю роль 'Центральный администратор'
        And Я нажимаю на пункт меню 'Подразделения'
        And Я нажимаю на кнопку 'Добавить подразделение'
        And Я ввожу данные в поле 'Наименование': 'Автотест'
        And Я ввожу данные в поле 'Тип': 'Управленческое подразделение'
        And Я ввожу данные в поле 'Статус': 'Открыто'
        And Я ввожу данные в поле 'Валюта': 'Рубль'
        And Я ввожу данные в поле 'Организация': 'ИП Абдухалимов Исломбек Шокиржон Угли'
        And Я ввожу данные в поле 'Максимальная сумма заказа без подтверждения': '5000'
        And Я ввожу данные в поле 'Логин почтового ящика': 'Test@test.com'
        And Я ввожу данные в поле 'Пароль почтового ящика': '123'
        And Я ввожу данные в поле 'Часовой пояс': '+03:00'
        And Я нажимаю кнопку 'Сохранить'

    Scenario: Удаление подразделения
        Given Я открываю страницу авторизации
        And Я ввожу логин и пароль учетной записи центрального администратора
        And Нажимаю кнопку 'ВХОД'
        And Я выбираю роль 'Центральный администратор'
        And Я нажимаю на пункт меню 'Подразделения'
        And Я нажимаю на кнопку 'Добавить подразделение'
        And Я ввожу данные в поле 'Наименование': 'Автотест1'
        And Я ввожу данные в поле 'Тип': 'Управленческое подразделение'
        And Я ввожу данные в поле 'Статус': 'Открыто'
        And Я ввожу данные в поле 'Валюта': 'Рубль'
        And Я ввожу данные в поле 'Организация': 'ИП Абдухалимов Исломбек Шокиржон Угли'
        And Я ввожу данные в поле 'Максимальная сумма заказа без подтверждения': '5000'
        And Я ввожу данные в поле 'Логин почтового ящика': 'Test@test.com'
        And Я ввожу данные в поле 'Пароль почтового ящика': '123'
        And Я ввожу данные в поле 'Часовой пояс': '+03:00'
        And Я нажимаю кнопку 'Сохранить'
        And Я закрываю сообщение об успешном создании подразделения
        And Я нажимаю на гамбургер напротив подразделения: 'Автотест1'
        And Я нажимаю на кнопку: 'Удалить подразделение'
        And Я нажимаю на кнопку подвтерждения: 'Удалить' в модальном окне
        And Я вижу всплывающее сообщение о том, что подразделение 'Автотест1' успешно удалено
        And Я не вижу удаленного подразделения 'Автотест1' в общем списке
        
    Scenario: Добавление торговой зоны
        Given Я открываю страницу авторизации
        And Я ввожу логин и пароль учетной записи центрального администратора
        And Нажимаю кнопку 'ВХОД'
        And Я выбираю роль 'Центральный администратор'
        And Я нажимаю на пункт меню 'Подразделения'
        And Я нажимаю на кнопку 'Добавить подразделение'
        And Я ввожу данные в поле 'Наименование': 'Автотест'
        And Я ввожу данные в поле 'Тип': 'Торговая зона'
        And Я ввожу данные в поле 'Статус': 'Открыто'
        And Я ввожу данные в поле 'Валюта': 'Рубль'
        And Я ввожу данные в поле 'Максимальное время в минутах доставки курьером': '30'
        And Я ввожу данные в поле 'Логин почтового ящика': 'Test@test.com'
        And Я ввожу данные в поле 'Пароль почтового ящика': '123'
        And Я ввожу данные в поле 'Часовой пояс': '+03:00'
        And Я нажимаю кнопку 'Сохранить'
        