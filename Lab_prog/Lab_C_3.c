/*
 * main.c
 *
 * Created: 1/13/2021 2:42:55 PM
 *  Author: ilyah
 */ 
#include <stdio.h>
#include <xc.h>

int main(void)
{
  int a[20] = {1,111,1,1,0};
  int k = 0, i = 0, j = 0, b = 0; 
  while (a[i] != 0){
	  if (a[i] > 255 || a[i] < 0) PORTB = 3;
	  i++;
	  if (i > 19) {
		  PORTB = 1;
		  break;}
  }
  i = 0;
  while ((a[i] != 0) || (PORTB > 0)){
	j = i;
	while(a[j] != 0){
		if (a[i] == a[j] && i != j) {
			b = 1;
			break;}
		j++;
	}
	j = i;
	while(j != -1){
		if (a[i] == a[j] && i != j) {
			b = 0;
			break;}
		j--;
	}
	k+=b;
	b = 0;
	i++;
	}
  while(1)
  { 
	PORTD = k;
  }
}
