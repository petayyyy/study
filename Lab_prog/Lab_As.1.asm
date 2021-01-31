;
; AssemblerApplication9.asm
;
; Created: 31.01.2021 16:03:51
; Author : ilyah
;

.include "m168def.inc"

.def MINIMUM = r16
.def MAXIMUM = r17
.def RESULT = r18
.def STAK = r19
.def I = r20
.def NUM = r21
.def HAG = r22

LDI STAK, low(RAMEND) 		; Создаю стак
OUT SPL, STAK
LDI STAK, high(RAMEND)
OUT SPH, STAK
CLR STAK

.DSEG		; Выделяю память ОЗУ под ответ и выходной массив
.ORG SRAM_Start
.BYTE 11

.CSEG  		; Начало программы
.ORG 0x07

ARRAY: .db 3, 0, 0, 0, 0, 0, 0, 0, 0, 0 		; Ввожу массив


ldi ZH, High(ARRAY*2)
ldi ZL, Low(ARRAY*2)

CLR I		; Обнуление переменных
CLR NUM
CLR RESULT
CLR MINIMUM
CLR MAXIMUM

LPM MINIMUM, Z+		; Беру первый элемент массива за минимум

FINDMIN:		; Ищу минимум массива
	CPI I, 9
	BREQ ENDMIN

	LPM NUM, Z+
	INC I

	CP NUM, MINIMUM
	BRLO NEWMIN
	RJMP FINDMIN

NEWMIN:			; Запоминаю минимум
	MOV MINIMUM, NUM
	RJMP FINDMIN

ENDMIN:

	ldi ZH, High(ARRAY*2)		; Начинаю заново перебирать массив
	ldi ZL, Low(ARRAY*2)
	LPM MAXIMUM, Z+		; Беру первый элемент за максимум
	CLR I
	LDI HAG, 6
	ADD MINIMUM, HAG		; Прибавляю 6 к минимому по условию задания
	BRBS 0, ERROR		; Проверяю на переполнение

FINDMAX:		; Ищу максимум
	CPI I, 9
	BREQ ENDMAX

	LPM NUM, Z+
	INC I

	CP NUM, MAXIMUM
	BRSH NEWMAX
	RJMP FINDMAX

NEWMAX:		; Запоминаю максисмум
	MOV MAXIMUM, NUM
	RJMP FINDMAX

ENDMAX:
	ldi ZH, High(ARRAY*2)		; Начинаю заново перебирать массив
	ldi ZL, Low(ARRAY*2)
	CLR I
	LDI HAG, 5
	SUB MAXIMUM, HAG		; Вычитаю 5 из максимума по условию задания
	BRBS 0, ERROR		; Проверяю на отрицательные значения

FINDNUM:		; Ищу количество подходящих чисел
	CPI I, 10
	BREQ ENDNUM

	LPM NUM, Z+
	INC I

	CP NUM, MAXIMUM
	BRLO FINDNUMM
	RJMP FINDNUM

FINDNUMM:
	CP NUM, MINIMUM
	BRSH PLUS
	RJMP FINDNUM

PLUS:		; Запоминаю подходящие числа в стак и обновляю их колияество
	INC RESULT
	PUSH NUM
	RJMP FINDNUM

ENDNUM:
	OUT PORTD, RESULT		; Вывожу количество подходящих чисел в порт D 
	STS  SRAM_START, RESULT			; Отправляю колличество подходящих чисел в ОЗУ 
	LDI  XL, LOW(SRAM_START + 1)  
	LDI  XH, HIGH(SRAM_START + 1)                
	CLR I

RAMPUSH:   
	INC  I 
	POP  NUM 
	ST   X+, NUM		; Отправляю значения в ОЗУ
	CP   I, RESULT
	BRNE RAMPUSH 
	RJMP END

END: RJMP END		; Конец программы

ERROR:		; Конец программы если нашлась ошибка
	LDI HAG, 2
	OUT PORTB, HAG		; Вывожу наличие ошибки(2) в порт B 
	RJMP ERROR
