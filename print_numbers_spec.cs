using System;
using NumbersLibrary;
using NUnit.Framework;

namespace PrintNumbers
{
    [TestFixture]
    public class print_numbers_spec
    {
        [Test]
        public void it_should_print_numbers_from_1_to_100()
        {
            var outputStream = new OutputMock();
            var sut = new NumberPrinter(outputStream);
            sut.PrintNumbers();

            Assert.That(outputStream.List.Count, Is.EqualTo(100));
        }

// *** this is how we started... normally I would delete this test in favor of the refactored one below...
//        [Test]
//        public void it_should_print_fiss_if_number_divisible_by_3()
//        {
//            var outputStream = new OutputMock();
//            var sut = new NumberPrinter(outputStream);
//            sut.PrintNumbers();
//
//            for (var i = 0; i < 100; i++)
//            {
//                if((i+1)%3 == 0) 
//                    Assert.That(outputStream.List[i], Is.EqualTo("fiss"));
//            }
//        }

        [Test]
        // now using method register instead of hard coding
        public void it_should_print_fiss_if_number_divisible_by_3()
        {
            const string text = "fiss";
            var outputStream = new OutputMock();
            var sut = new NumberPrinter(outputStream);
            sut.Register(3, text);
            sut.PrintNumbers();

            for (var i = 0; i < 100; i++)
            {
                if ((i + 1) % 3 == 0)
                    Assert.That(outputStream.List[i], Is.EqualTo(text));
            }
        }

// *** this is how we started... normally I would delete this test in favor of the refactored one below...
///        [Test]
//        public void it_should_print_buz_if_number_divisible_by_5_but_not_by_3()
//        {
//            // Note: the Product Owner has told me that 3 has precedence over 5
//            var outputStream = new OutputMock();
//            var sut = new NumberPrinter(outputStream);
//            sut.PrintNumbers();
//
//            for (var i = 0; i < 100; i++)
//            {
//                if ((i + 1) % 5 == 0 && (i+1)%3 != 0)
//                    Assert.That(outputStream.List[i], Is.EqualTo("buz"));
//            }
//        }

        [Test]
        // now using method register instead of hard coding
        public void it_should_print_buz_if_number_divisible_by_5_but_not_by_3()
        {
            const string text = "buz";
            // Note: the Product Owner has told me that 3 has precedence over 5
            var outputStream = new OutputMock();
            var sut = new NumberPrinter(outputStream);
            sut.Register(5, text);
            sut.Register(3, "fiss");
            sut.PrintNumbers();

            for (var i = 0; i < 100; i++)
            {
                if ((i + 1) % 5 == 0 && (i+1)%3 != 0)
                    Assert.That(outputStream.List[i], Is.EqualTo(text));
            }
        }

        [Test]
        public void it_should_be_configurable_by_any_divisor_output_string_pair()
        {
            const string text = "seven is great";
            var outputStream = new OutputMock();
            var sut = new NumberPrinter(outputStream);
            sut.Register(7, text);
            sut.PrintNumbers();

            Assert.That(outputStream.List[6], Is.EqualTo(text));
        }

        [Test]
        public void it_should_give_precedence_to_the_last_registered_pair_over_all_others()
        {
            const string text = "seven is great";
            var outputStream = new OutputMock();
            var sut = new NumberPrinter(outputStream);
            sut.Register(3, "three is first");
            sut.Register(5, "five is second");
            sut.Register(7, text);
            sut.PrintNumbers();

            Assert.That(outputStream.List[20], Is.EqualTo(text));
            Assert.That(outputStream.List[41], Is.EqualTo(text));

            Assert.That(outputStream.List[34], Is.EqualTo(text));
            Assert.That(outputStream.List[69], Is.EqualTo(text));
        }

        [Test]
        public void it_should_allow_lower_and_upper_number_limit_to_be_set()
        {
            var outputStream = new OutputMock();
            var sut = new NumberPrinter(outputStream);
            sut.PrintNumbers(10, 80);
            Assert.That(outputStream.List.Count, Is.EqualTo(71));
            Assert.That(outputStream.List[0], Is.EqualTo("10"));
            Assert.That(outputStream.List[70], Is.EqualTo("80"));
        }

        [Test]
        public void it_should_only_allow_positive_lower_limit_to_be_set()
        {
            var outputStream = new OutputMock();
            var sut = new NumberPrinter(outputStream);
            Assert.Throws<ArgumentException>(() => sut.PrintNumbers(-10, 80));
            Assert.Throws<ArgumentException>(() => sut.PrintNumbers(0, 80));
        }

        [Test]
        public void it_should_only_allow_upper_limit_be_greater_than_lower_limit()
        {
            var outputStream = new OutputMock();
            var sut = new NumberPrinter(outputStream);
            Assert.Throws<ArgumentException>(() => sut.PrintNumbers(19, 10));
            Assert.Throws<ArgumentException>(() => sut.PrintNumbers(4, 4));
        }

        [Test]
        public void it_should_allow_only_positive_divisors()
        {
            var outputStream = new OutputMock();
            var sut = new NumberPrinter(outputStream);
            Assert.Throws<ArgumentException>(() => sut.Register(-101, "foo"));
            Assert.Throws<ArgumentException>(() => sut.Register(0, "foo"));
        }

        [Test]
        public void it_should_not_allow_to_register_divisor_with_empty_or_null_associated_text()
        {
            var outputStream = new OutputMock();
            var sut = new NumberPrinter(outputStream);
            Assert.Throws<ArgumentException>(() => sut.Register(13, ""));
            Assert.Throws<ArgumentException>(() => sut.Register(13, null));
        }

        [Test]
        public void it_should_override_the_old_registered_text_if_registering_the_same_divisor_again()
        {
            var outputStream = new OutputMock();
            var sut = new NumberPrinter(outputStream);
            sut.Register(3, "fiss");
            sut.Register(3, "baz");
            sut.PrintNumbers();
            Assert.That(outputStream.List[2], Is.EqualTo("baz"));
        }

        [Test]
        // visual test only!
        public void it_should_display_correctly_on_console()
        {
            var outputStream = new ConsoleWrapper();
            var sut = new NumberPrinter(outputStream);
            sut.Register(3, "three is first");
            sut.Register(5, "five is second");
            sut.Register(7, "seven is dominant");
            sut.PrintNumbers(10, 80);
        }
    }
}