/*
 * main.c
 *
 * Created: 1/13/2021 2:42:55 PM
 *  Author: ilyah
 */ 
#include <stdio.h>
#include <stdbool.h>
#include <xc.h>

int main(void)
{
  int a[5] = {1,111,1,1,0}; // объявлен массив a из 5 элементов
  int n = 5;
  
  n--;
  int k = 0;
  bool flag = false;
  for (int i = 0; i < n; i++){
      for (int j = 0; j < n; j++){
        if (a[i] == a[j] && i != j){
            flag = true;
            break;
        }
      }
      if (flag){
          k++;
          printf("%d/n",a[i]);
          flag = false;
      }
  }
  printf("%d/n", n - k);
  // Вывод элементов массива
  for (int i = 0; i<5; i++)
    printf("%d ", a[i]); // пробел в формате печати обязателен
	
	while(1)
    {
        PORTD = n - k;
    }
}
