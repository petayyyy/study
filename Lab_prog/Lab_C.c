#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdbool.h>
int main()
{
  int a[5] = {0}; // объявлен массив a из 5 элементов
  int n;
  // Ввод элементов массива
  for (n = 0; n<5; n++) 
  {
    printf("a[%d] = ", n);
    scanf("%d", &a[n]); // &a[i] - адрес i-го элемента массива
    if (a[n] == 0) break;
  }
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
  //getchar(); getchar();
  return 0;
}
