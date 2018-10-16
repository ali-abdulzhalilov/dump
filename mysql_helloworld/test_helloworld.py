from unittest import TestCase
from helloworld import pew

class PewTestCase(TestCase):
	def test_pew(self):
		self.assertEqual(pew(), (1, 'A'))