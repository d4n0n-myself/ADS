# Final Report about task

В основу реализации взят односвязный список, построенный на элементах класса `MatrixElement`, содержащих значения строк, столбцов, и значений. Коллекция хранится в неотсортированном виде, что позволяет таким методам, как `Transpose()` работать эффективно.

### Методы:
* Создан интерфейс `IMatrix.cs` для проверки наличия всех требуемых методов. 

* Создан конструктор `Matrix(int[][] matrix)` ([link to Matrix.cs](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/Matrix.cs)) для создания матрицы из исходного массива массивов. Создаёт и передаёт значения всех ненулевых элементов матрицы с помощью вспомогательного метода InternalInsert. Он ищет существующий элемент и меняет его значение, либо создает новый и приписывает в конец списка. К нему создан дополнительный конструктор `Matrix(int size)` для пустой матрицы по её размеру. Он устанавливает размерность без добавления элементов.

 * Создан метод `int[][] Decode()` и переименован в метод `int[][] GetMartix()  `.
Созданы соответствующие `nUnit` тесты в [файле](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/DecodeMethodTests.cs).
  Обрабатываем новую матрицу (создаём все массивы, чтобы исключить обращение к null) и с помощью индексатора заполняем её.
   * Сложность: O(n), где n - кол-во ненулевых элементов в матрице.

* Создан метод `void Insert(int i,int j,int value)` и соответствующие `nUnit` тесты в [файле](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/InsertMethodTests.cs).
  Проверяет значение на ноль и определяет значение в элемент через соответсвтующие методы `InternalInsert` или `Delete`.
  * Сложность: O(n), где n - кол-во ненулевых элементов в матрице.

* Создан метод `void Delete(int i,int j)` и соответствующие `nUnit` тесты в [файле](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/DeleteMethodTests.cs). 
  Ищет элемент в позиции `[lineIndex,columnindex]` и, при его существовании, удаляет его.
  * Сложность: O(n), где n - кол-во ненулевых элементов в матрице.

* Создан метод `List<int> MinList` и переименован в `List<int> GetListOfMinimalOfColumns()`. Соответствующие `nUnit` тесты в [файле](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/ListOfMinimaOfColumnsMethodTests.cs).
  С помощью вспомогательного массива ищем минимальные значения в закодированной матрице.
  * Сложность: O(n), где n - кол-во ненулевых элементов в матрице.

* Создан метод `int DiagSum()` и соответствующие `nUnit` тесты в [файле](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/DiagSumMethodTests.cs).
  Цикл по всем элементам списка ищет удовлетворяющие условиям элементы и добавляет в переменную.
  * Сложность: O(n), где n - кол-во ненулевых элементов в матрице.

* Создан метод `void Transp()` и переименован в `void Transpose()`. Cоответствующие `nUnit` тесты в [файле](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/TransposeMethodTests.cs).
  Для каждого элемента списка меняем значения Column и Line.
  * Сложность: O(n), где n - кол-во ненулевых элементов в матрице.

* Создан метод `void ColsSum(int column1, int column2)` и переименован в `void MakeTwoColumnsSum(column1, column2)` соответствующие `nUnit` тесты в [файле](https://github.com/d4n0n-myself/ADS/blob/master/MatrixCode/MatrixCode/MakeTwoColumnsMethodTests.cs).
  Внешний цикл ищет элементы с `Column == column1`, внутренний ищет элементы с `Column == column2` и на той же строке, что и элемент 1 цикла.
  * Сложность: O(n^2), где n - кол-во ненулевых элементов в матрице.


Все созданные мной `nUnit` тесты выполняются корректно иcпользуя методы из `Matrix.cs`.
![Proof](https://github.com/d4n0n-myself/ADS/blob/master/%D0%A1%D0%BD%D0%B8%D0%BC%D0%BE%D0%BA%20%D1%8D%D0%BA%D1%80%D0%B0%D0%BD%D0%B0%202018-03-08%20%D0%B2%2012.56.25.png)
