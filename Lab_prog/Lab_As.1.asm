.include "m168def.inc"

.def minim = r16
.def element = r17
.def count = r18
.def temp = r19
.def maxim = r20
.def counter = r21
.def var = r22
.def place = r23
.def numb_ele = r24
.def count_fit = r25

ldi temp, low(RAMEND) ;Указатель стека указывает
out SPL, temp ;на последний адрес ОЗУ
ldi temp, high(RAMEND)
out SPH, temp
clr temp

.dseg
array2: .BYTE 11

.cseg

array: .db 21, 4, 10, 2, 100, 30, 202, 12, 6, 7


ldi ZH, High(array*2)
ldi ZL, Low(array*2)



ldi count, 0
ldi counter, 0
ldi count_fit, 0
ldi numb_ele, 0


lpm minim, Z+
loop:
cpi count, 10
breq loop1

lpm element, Z+
inc count
cp element, minim
brlo newmin
rjmp loop

newmin:
mov minim, element
clr element
rjmp loop

loop1:
ldi ZL, Low(array*2)
ldi ZH, High(array*2)
lpm maxim, Z+




ldi count, 0

loop2:
inc count
cpi count, 10
breq loop3

lpm element, Z+
cp element, maxim
brlo loop2
breq loop2
rjmp newmax


newmax:
mov maxim, element
clr element
rjmp loop2


loop3:
clr element
ldi ZL, Low(array*2)
ldi ZH, High(array*2)
ldi count, 0
rjmp compare
compare:
lpm element, Z+
inc counter
cpi counter, 10
breq place_element


mov temp, element
mov var, maxim
mov count, element

sub element, minim
cpi element, 6
brlo compare
breq compare
sub var, count
cpi var, 5
brlo compare
breq compare
rjmp placefit


placefit:
inc count_fit
push temp
rjmp compare



place_element:
sts array2, count_fit
ldi XL, LOW(array2 + 1)
ldi XH, HIGH(array2 + 1)

placing:
inc numb_ele
pop place
st X+, place
cp numb_ele, count_fit
brne placing
end:
nop

rjmp end
