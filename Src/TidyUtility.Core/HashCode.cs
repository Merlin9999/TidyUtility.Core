 #nullable disable
 using System;
 using System.Collections.Generic;
 using System.ComponentModel;

 namespace TidyUtility.Core
{
    // Adapted from: https://rehansaeed.com/gethashcode-made-easy/

    // License Note taken directly from the page above on 2019-10-19:
    //    "Please consider the code as MIT licensed, do good with it and be excellent to each other!"
    //
    // I will attempt to honor the author's intent as written. According to this website, the author
    // of this article is named Muhammad Rehan Saeed, so he is the copyright holder. Below is the
    // generic text of what is commonly known as the MIT License.  It was taken from
    // https://opensource.org/licenses/MIT on 2019-10-19.
    //
    // Copyright <YEAR> <COPYRIGHT HOLDER>
    //
    // Permission is hereby granted, free of charge, to any person obtaining a copy of this software
    // and associated documentation files (the "Software"), to deal in the Software without restriction,
    // including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
    // and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
    // subject to the following conditions:
    //
    // The above copyright notice and this permission notice shall be included in all copies or substantial
    // portions of the Software.
    //
    // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT
    // LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
    // IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
    // WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
    // SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


    /// <summary>
    /// A hash code used to help with implementing <see cref="object.GetHashCode()"/>.
    /// </summary>
    public struct HashCode : IEquatable<HashCode>
    {
        private const int EmptyCollectionPrimeNumber = 19;
        private readonly int value;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashCode"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        private HashCode(int value) => this.value = value;

        /// <summary>
        /// Performs an implicit conversion from <see cref="HashCode"/> to <see cref="int"/>.
        /// </summary>
        /// <param name="hashCode">The hash code.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator int(HashCode hashCode) => hashCode.value;

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(HashCode left, HashCode right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(HashCode left, HashCode right) => !(left == right);

        /// <summary>
        /// Takes the hash code of the specified item.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>The new hash code.</returns>
        public static HashCode Of<T>(T item) => new HashCode(GetHashCode(item));

        /// <summary>
        /// Takes the hash code of the specified items.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="items">The collection.</param>
        /// <returns>The new hash code.</returns>
        public static HashCode OfEach<T>(IEnumerable<T> items) =>
            items == null ? new HashCode(0) : new HashCode(GetHashCode(items, 0));

        /// <summary>
        /// Adds the hash code of the specified item.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>The new hash code.</returns>
        public HashCode And<T>(T item) => new HashCode(CombineHashCodes(this.value, GetHashCode(item)));

        /// <summary>
        /// Adds the hash code of the specified items in the collection.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="items">The collection.</param>
        /// <returns>The new hash code.</returns>
        public HashCode AndEach<T>(IEnumerable<T> items)
        {
            if (items == null)
            {
                return new HashCode(this.value);
            }

            return new HashCode(GetHashCode(items, this.value));
        }

        /// <inheritdoc />
        public bool Equals(HashCode other) => this.value.Equals(other.value);

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is HashCode)
            {
                return this.Equals((HashCode)obj);
            }

            return false;
        }

        /// <summary>
        /// Throws <see cref="NotSupportedException" />.
        /// </summary>
        /// <returns>Does not return.</returns>
        /// <exception cref="NotSupportedException">Implicitly convert this struct to an <see cref="int" /> to get the hash code.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() =>
            throw new NotSupportedException("Implicitly convert this struct to an int to get the hash code.");

        private static int CombineHashCodes(int h1, int h2)
        {
            unchecked
            {
                // Code copied from System.Tuple so it must be the best way to combine hash codes or at least a good one.
                return ((h1 << 5) + h1) ^ h2;
            }
        }

        private static int GetHashCode<T>(T item) => item?.GetHashCode() ?? 0;

        private static int GetHashCode<T>(IEnumerable<T> items, int startHashCode)
        {
            var temp = startHashCode;

            var enumerator = items.GetEnumerator();
            if (enumerator.MoveNext())
            {
                temp = CombineHashCodes(temp, GetHashCode(enumerator.Current));

                while (enumerator.MoveNext())
                {
                    temp = CombineHashCodes(temp, GetHashCode(enumerator.Current));
                }
            }
            else
            {
                temp = CombineHashCodes(temp, EmptyCollectionPrimeNumber);
            }

            return temp;
        }
    }
}