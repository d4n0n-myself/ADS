# Final Report about task


### Этапы(содержание):
* Создан интерфейс `IMatrix.cs` для проверки наличия всех требуемых методов. 

* Создан конструктор `Matrix(int[][] matrix)` ([link to Matrix.cs](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/Matrix.cs)) для создания матрицы из исходного массива массивов. К нему создан дополнительный конструктор `Matrix(int size)` для пустой матрицы по её размеру.

 * Создан метод `int[][] Decode()` и переименован в метод `int[][] GetMartix()`.
Созданы соответствующие `nUnit` тесты в [файле](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/DecodeMethodTests.cs).

* Создан метод `void Insert(int i,int j,int value)` и соответствующие `nUnit` тесты в [файле](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/InsertMethodTests.cs).

* Создан метод `void Delete(int i,int j)` и соответствующие `nUnit` тесты в [файле](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/DeleteMethodTests.cs).

* Создан метод `List<int> MinList` и переименован в `List<int> GetListOfMinimalOfColumns()`. Соответствующие `nUnit` тесты в [файле](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/ListOfMinimaOfColumnsMethodTests.cs).

* Создан метод `int DiagSum()` и соответствующие `nUnit` тесты в [файле](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/DiagSumMethodTests.cs).

* Создан метод `void Transp()` и переименован в `void Transpose()`. Cоответствующие `nUnit` тесты в [файле](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/TransposeMethodTests.cs).

* Создан метод `void ColsSum(int column1, int column2)` и переименован в `void MakeTwoColumnsSum(column1, column2)` соответствующие `nUnit` тесты в [файле](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/MakeTwoColumnsMethodTests.cs).


Все созданные мной `nUnit` тесты выполняются корректно иcпользуя методы из `Matrix.cs`.
![Proof](https://github.com/d4n0n-myself/ADS/blob/master/%D0%A1%D0%BD%D0%B8%D0%BC%D0%BE%D0%BA%20%D1%8D%D0%BA%D1%80%D0%B0%D0%BD%D0%B0%202018-03-08%20%D0%B2%2012.56.25.png)
