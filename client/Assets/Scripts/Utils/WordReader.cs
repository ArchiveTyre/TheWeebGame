using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class WordReader {
	private IEnumerator<char> m_Source;
	public WordReader(string source) {
		m_Source = source.GetEnumerator();
	}

	private bool shouldSkip(char c) {
		return char.IsWhiteSpace(c);
	}

	public string readString() {
		if (m_Source.MoveNext()) {
			while(shouldSkip(m_Source.Current)) {
				if (!m_Source.MoveNext()) {
					return null;
				}
			}

			StringBuilder builder = new StringBuilder();

			if (m_Source.Current == '"') {
				while(m_Source.MoveNext() && m_Source.Current != '"') {
					builder.Append(m_Source.Current);
				}
				return builder.ToString();
			}
			else {
				return null;
			}
		}
		else {
			return null;
		}
	}

	public string readRest() {
		StringBuilder builder = new StringBuilder();
		builder.Append(m_Source.Current);
		while(m_Source.MoveNext()) {
			builder.Append(m_Source.Current);
		}
		return builder.ToString();
	}

	public string readWord() {
		if (m_Source.MoveNext()) {
			while(shouldSkip(m_Source.Current)) {
				if (!m_Source.MoveNext()) {
					return null;
				}
			}

			StringBuilder builder = new StringBuilder();

			if (char.IsLetterOrDigit(m_Source.Current)) {
				while (!char.IsWhiteSpace(m_Source.Current)) {
					builder.Append(m_Source.Current);
					if (!m_Source.MoveNext()) {
						return builder.ToString();
					}
				}
				return builder.ToString();
			}
			else {
				return null;
			}
		}
		else {
			return null;
		}
	}

	public int readInt() {
		return int.Parse(readWord());
	}
}
