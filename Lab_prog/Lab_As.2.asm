.include "m168def.inc"
;F = A * (B + 4) / 5 + (C – 1) / 2 * (D + 2)
;F = A B 4 + * 5 / C – 1 2 / D 2 + * +

.equ A = 10
.equ B = 10
.equ C = 10
.equ D = 10
.def REG1 = r18
.def REG2 = r19
.def RESULT = r20
.def TEMP = r16
.def COUNT = r17
.DEF DEBUG = r0

ldi TEMP, low(RAMEND)
out SPL, TEMP
ldi TEMP, high(RAMEND)
out SPH, TEMP
clr TEMP

.cseg
.org 0x07

LDI REG1, B				; REG1 = B, REG2 = 0, RESULT = 0 ; COUNT = 0 ; DEBUG = 0; STACK: -
LDI REG2, 4				; REG1 = B, REG2 = 4, RESULT = 0 ; COUNT = 0 ; DEBUG = 0; STACK: -
ADD REG1, REG2			; REG1 = B+4, REG2 = 4, RESULT = 0 ; COUNT = 0 ; DEBUG = 0; STACK: -
BRBS 0, error

LDI REG2, A				; REG1 = B+4, REG2 = A, RESULT = 0 ; COUNT = 0 ; DEBUG = 0; STACK: -
MUL REG1,REG2			; REG1 = B+4, REG2 = A, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A; STACK: -
MOV REG1,DEBUG			; REG1 = (B+4) * A, REG2 = A, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A; STACK: -

LDI REG2, 5				; REG1 = (B+4) * A, REG2 = 5, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A;STACK: -
RCALL DELETE			; REG1 = ((B+4) * A)/5, REG2 = 5, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A;STACK: -
PUSH REG1				; REG1 = ((B+4) * A)/5, REG2 = 5, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A;STACK: ((B+4) * A)/5

LDI REG1, C				; REG1 = C, REG2 = 5, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A;STACK: ((B+4) * A)/5
LDI REG2, 1				; REG1 = C, REG2 = 1, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A;STACK: ((B+4) * A)/5
SUB REG1, REG2			; REG1 = C-1, REG2 = 1, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A;STACK: ((B+4) * A)/5
PUSH REG1				; REG1 = C-1, REG2 = 1, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A;STACK: ((B+4) * A)/5, C-1
BRBS 2, error

LDI REG1, D				; REG1 = D, REG2 = 1, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A;STACK: ((B+4) * A)/5, C-1
LDI REG2, 2				; REG1 = D, REG2 = 2, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A;STACK: ((B+4) * A)/5, C-1
ADD REG1, REG2			; REG1 = D+2, REG2 = 2, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A;STACK: ((B+4) * A)/5, C-1

POP REG2				; REG1 = D+2, REG2 = C-1, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A;STACK: ((B+4) * A)/5
MUL REG1,REG2			; REG1 = D+2, REG2 = C-1, RESULT = 0 ; COUNT = 0 ;DEBUG = (D+2) * (C-1); STACK: ((B+4) * A)/5
MOV REG1,DEBUG			; REG1 = (D+2) * (C-1), REG2 = C-1, RESULT = 0 ; COUNT = 0 ;DEBUG = (D+2) * (C-1); STACK: ((B+4) * A)/5

LDI REG2, 2				; REG1 = (D+2) * (C-1), REG2 = 2, RESULT = 0 ; COUNT = 0 ;DEBUG = (D+2) * (C-1); STACK: ((B+4) * A)/5
RCALL DELETE			; REG1 = (D+2) * (C-1)/2, REG2 = 2, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A;STACK: ((B+4) * A)/5

POP REG2				; REG1 = (D+2)*(C-1)/2, REG2 = ((B+4) * A)/5, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A; STACK: -
ADD REG1, REG2			; REG1 = (D+2)*(C-1)/2 + ((B+4) * A)/5, REG2 = ((B+4) * A)/5, RESULT = 0 ; COUNT = 0 ; DEBUG = (B+4) * A; STACK: -
BRBS 0, error

MOV RESULT, REG1		; REG1 = (D+2)*(C-1)/2 + ((B+4) * A)/5, REG2 = ((B+4) * A)/5, RESULT = (D+2)*(C-1)/2 + ((B+4) * A)/5 ; COUNT = 0 ; DEBUG =  (B+4) * A; STACK: -
CLR REG1				; REG1 = 0 , REG2 = ((B+4) * A)/5, RESULT = (D+2)*(C-1)/2 + ((B+4) * A)/5 ; COUNT = 0 ; DEBUG = (B+4) * A; STACK: -
CLR REG2				; REG1 = 0 , REG2 = 0 , RESULT = (D+2)*(C-1)/2 + ((B+4) * A)/5 ; COUNT = 0 ; DEBUG = (B+4) * A; STACK: -
CLR DEBUG				; REG1 = 0 , REG2 = 0 , RESULT = (D+2)*(C-1)/2 + ((B+4) * A)/5 ; COUNT = 0 ; DEBUG = 0; STACK: -
RCALL LOOP

OUT PORTD, RESULT
LOOP: RJMP LOOP
ERROR: RJMP ERROR

DELETE:
CP REG1, REG2
BRSH DEL
RJMP PASS

DEL:
SUB REG1, REG2
INC COUNT
RJMP DELETE

PASS:
MOV REG1, COUNT
CLR COUNT
RET
