namespace Yort.OnlineEftpos
{
	/// <summary>
	/// Common interface for components that can validate and normalise user input for a particular type of payer id.
	/// </summary>
	public interface IPayerIdType
	{

		/// <summary>
		/// Returns the name of this payer id type as known by the Online EFTPOS API.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Returns a more friendly, human readable version of <see cref="Name"/> for displaying to customers/users.
		/// </summary>
		string DisplayName { get; }

		/// <summary>
		/// Takes the given <paramref name="userInput"/> and attempts to 'normalise' it, that is remove unneccesary characters, format and validate the result.
		/// </summary>
		/// <param name="userInput">The input to be corrected.</param>
		/// <returns>A string containing the normalised form of the input.</returns>
		/// <exception cref="OnlineEftposInvalidDataException">Thrown if the result cannot be normalised or results in an invalid value.</exception>
		string Normalize(string userInput);
		/// <summary>
		/// Returns true if the specified <paramref name="value"/> is a valid payer id of this type.
		/// </summary>
		/// <remarks>
		/// <para>This method does not modify the input in any way, use the <see cref="Normalize(string)"/> method to sanitise the input before calling this method.</para>
		/// </remarks>
		/// <param name="value"></param>
		/// <returns>True if <paramref name="value"/> is a valid payer id of this type.</returns>
		bool IsValid(string value);
	}
}