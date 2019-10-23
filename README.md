# MyGenericList

## Lehrziele:
  *	Generics
  * Sortierung 
    * Bubble Sort (Wiederholung)
    * Natürliche Sortierung (IComparable vs. IComparator)


Die Liste aus der Vorwoche ist so umzugestalten, dass das Interface `IList<T>` implementiert wird.

VORSICHT: In einigen Details unterscheidet sich `IList<T>` von `List<T>`. Bitte in den Microsoft-Definitionen nachlesen.

Implementieren Sie zusätzlich eine `Sort(...)`-Methode mithilfe des Bubble-Sort Algorithmus, welche die Elemente nach der natürlichen Sortierung der Datenklasse ("Nach Nachnamen aufsteigend") ordnet.

Zusätzlich soll die Liste auch nach anderen Kriterien sortiert werden können, welche nicht der natürlichen Sortierung der Datenklasse entsprechen.

Überladen Sie dazu die Sort-Methode mit einem Parameter IComparer. In den Testfällen werden folgende IComparer-Klassen vorausgesetzt:
  1. Nach dem Vornamen aufsteigend
  1. Nach dem Alter absteigend

Es müssen natürlich auch die Unittests entsprechend angepasst werden, damit das geänderte Interface geprüft werden kann. Achten Sie auf eine möglichst hohe Codeabdeckung (Coverage) durch Ihre Tests.

## Hinweise:

1. `MyListEnumerator<T>`
    * Der Enumerator muss `IDisposable` implementieren und daher die Methode `Dispose()` anbieten. Da wir aber keine schwergewichtigen Ressourcen (wie zum Beispiel Datenbankverbindungen) verwalten, müssen wir nichts freigeben.
    * `IEnumerator<T>` verlangt auch, dass wir bei Current auch die nicht generische Version unterstützen

        ``` csharp
        object IEnumerator.Current => Current;

        public T Current => _actualNode.DataObject;
        ```

1. `MyList<T>` 
    * Es muss die generische und die nicht generische Variante eines Enumerators geliefert werden.

        ```csharp
        public IEnumerator<T> GetEnumerator() => new MyListEnumerator<T>(_head);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        ```
 
